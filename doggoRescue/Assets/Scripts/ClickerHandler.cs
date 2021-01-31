using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickerHandler : MonoBehaviour
{
    public static ClickerHandler inst;

    public Animator myAnim;
    public bool canMove;
    bool ableToMove;
    bool moveCamera, cameraMoving;

    public bool canSniff = true;
    public float sniffCooldown = 30F;

    NavMeshAgent myAgent;

    [Header("UI")]
    public GameObject UIobject;
    public GameObject clickedObject;
    public GameObject borkObject;

    [Header("Camera")]
    public Transform cameraTransform;
    Vector3 cameraOffset;
    float cameraTime;

    [Header("Hooman")]
    public NavMeshAgent hoomanAgent;

    [Header("Reset")]
    Vector3 startingPos;
    public Animator fader;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        myAgent = GetComponent<NavMeshAgent>();
        cameraOffset = new Vector3(cameraTransform.position.x - transform.position.x, cameraTransform.position.y - transform.position.y, cameraTransform.position.z - transform.position.z);
        startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) CheckMovement();

        if (moveCamera)
        {
            cameraTime += Time.deltaTime;
            if (!isMoving())
            {
                AudioManager.inst.PlayWalk(false);
                moveCamera = false;
                clickedObject.SetActive(false);
                StartCoroutine(StopCamera());
            }
        }
        if(cameraMoving && cameraTime > 7F) cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime);
        else if (cameraMoving) cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime * 0.8F);


        float speed = Mathf.Abs(myAgent.velocity.x) + Mathf.Abs(myAgent.velocity.z);
        myAnim.SetFloat("speed", speed);

        if (Input.GetKeyDown(KeyCode.Space)) Dig();
        //if (Input.GetKeyDown(KeyCode.Space) && !isMoving()) Dig();
        //else if (Input.GetKeyDown(KeyCode.Space) && isMoving()) Debug.Log("I can't dig I'm moving!");

        if (!canSniff)
        {
            sniffCooldown -= Time.deltaTime;
            if(sniffCooldown < 0)
            {
                canSniff = true;
                sniffCooldown = 10F;
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) ReturnToTown();
    }

    void CheckMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit thing;
        if (Physics.Raycast(ray, out thing, 300, LayerMask.GetMask("Ground")))
        {
            UIobject.SetActive(true);
            UIobject.transform.position = new Vector3(thing.point.x, thing.point.y + 0.2F, thing.point.z);

            ableToMove = true;
        }
        else
        {
            // turn off clicker object
            UIobject.SetActive(false);

            ableToMove = false;
        }

        if (ableToMove && Input.GetMouseButton(0))
        {
            Vector3 pos = new Vector3(UIobject.transform.position.x, UIobject.transform.position.y, UIobject.transform.position.z);
            //Debug.Log(pos);

            clickedObject.transform.position = pos;
            clickedObject.SetActive(true);

            StartCoroutine(MoveTowards(pos));
        }
        else if(ableToMove && Input.GetMouseButtonDown(1))
        {
            //bork
            Debug.LogWarning("BORK");
            AudioManager.inst.PlayBark();
            Vector3 pos = new Vector3(UIobject.transform.position.x, UIobject.transform.position.y, UIobject.transform.position.z);

            borkObject.SetActive(true);
            borkObject.transform.position = pos;

            hoomanAgent.GetComponent<HoomanMover>().BorkCommand(pos, borkObject);
        }
    }

    public IEnumerator MoveTowards(Vector3 position)
    {
        yield return new WaitForFixedUpdate();
        if (!isMoving()) AudioManager.inst.PlayWalk(true);

        myAgent.SetDestination(position);
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.2F);

        if (!moveCamera)
        {
            cameraTime = 0;
            moveCamera = cameraMoving = true;
        }
        hoomanAgent.GetComponent<HoomanMover>().StartMovement();
    }

    IEnumerator StopCamera()
    {
        yield return new WaitForSeconds(cameraTime * 0.1F);
        if (!isMoving())
        {
            cameraMoving = false;
            Debug.Log("stopping camera");
        }
        else Debug.Log("Dog is still moving, going to keep the camera going");
    }

    public bool isMoving()
    {
        //Debug.Log(myAgent.remainingDistance);
        return myAgent.remainingDistance > 0.1F;
    }

    void Dig()
    {
        //dig animation

       // if(isMoving())   StartCoroutine(MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z)));

        Collider[] interactables = Physics.OverlapSphere(transform.position, 1F, LayerMask.GetMask("Interactable"));
        if (interactables.Length == 0)
        {
            //Debug.Log("no interact");
           if (canSniff)
            {
                // sniff
                GetComponent<MissionTracker>().SmellTrail();
                canSniff = false;
            }
        }
        else
        {
            bool dig = false;
            foreach (Collider c in interactables)
            {
                if (c.GetComponent<BulletinBoard>()) return;

                if (c.GetComponent<DoggoDig>())
                {
                    if (c.GetComponent<DoggoDig>().Interact(this) && !dig)
                    {
                        AudioManager.inst.PlayDig();
                        dig = true;
                    }
                }
            }

            if (!dig && canSniff)
            {
                //sniff
                GetComponent<MissionTracker>().SmellTrail();
                canSniff = false;
            }
        }
    }

    public void ReturnToTown()
    {
        if (fader) fader.SetTrigger("fade");
        if (Menus.inst) Menus.inst.resume();

        myAgent.SetDestination(startingPos);
        
        Vector3 hoomanPos = new Vector3(startingPos.x, startingPos.y, startingPos.z + 4F);
        hoomanAgent.GetComponent<HoomanMover>().BorkCommand(hoomanPos, borkObject);

        StartCoroutine(TeleportObjects(hoomanPos));
    }

    IEnumerator TeleportObjects(Vector3 hoomanPos)
    {
        yield return new WaitForSeconds(1F);

        transform.position = startingPos;
        cameraTransform.position = startingPos + cameraOffset;
        cameraMoving = moveCamera = false;
        hoomanAgent.transform.position = hoomanPos;
        AudioManager.inst.PlayWalk(false);
    }
}
