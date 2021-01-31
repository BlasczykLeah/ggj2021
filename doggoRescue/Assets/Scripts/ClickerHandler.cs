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

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        myAgent = GetComponent<NavMeshAgent>();
        cameraOffset = new Vector3(cameraTransform.position.x - transform.position.x, cameraTransform.position.y - transform.position.y, cameraTransform.position.z - transform.position.z);
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
                moveCamera = false;
                clickedObject.SetActive(false);
                StartCoroutine(StopCamera());
            }
        }
        if(cameraMoving && cameraTime > 7F) cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime);
        else if (cameraMoving) cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime * 0.8F);


        float speed = Mathf.Abs(myAgent.velocity.x) + Mathf.Abs(myAgent.velocity.z);
        myAnim.SetFloat("speed", speed);

        if (Input.GetKeyDown(KeyCode.Space) && !isMoving()) Dig();
        else if (Input.GetKeyDown(KeyCode.Space) && isMoving()) Debug.Log("I can't dig I'm moving!");
    }

    private void FixedUpdate()
    {
       // if (!myAgent.isStopped)
            //cameraTransform.position = new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z);\
          //  cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime * 0.8F);
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
            Vector3 pos = new Vector3(UIobject.transform.position.x, UIobject.transform.position.y, UIobject.transform.position.z);

            borkObject.SetActive(true);
            borkObject.transform.position = pos;

            hoomanAgent.GetComponent<HoomanMover>().BorkCommand(pos, borkObject);
        }
    }

    IEnumerator MoveTowards(Vector3 position)
    {
        yield return new WaitForFixedUpdate();

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
        cameraMoving = false;
    }

    public bool isMoving()
    {
        //Debug.Log(myAgent.remainingDistance);
        return myAgent.remainingDistance > 0.1F;
    }

    void Dig()
    {
        //dig animation

        Collider[] interactables = Physics.OverlapSphere(transform.position, 1F, LayerMask.GetMask("Interactable"));
        if (interactables.Length == 0)
        {
            Debug.Log("no interact");
            return;
        }
        foreach(Collider c in interactables)
        {
            c.GetComponent<Interactable>().Interact(this);
        }

    }
}
