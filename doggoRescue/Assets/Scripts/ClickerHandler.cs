using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickerHandler : MonoBehaviour
{
    public static ClickerHandler thing;

    public bool canMove;
    bool ableToMove;
    bool moveCamera;

    NavMeshAgent myAgent;

    [Header("UI")]
    public GameObject UIobject;
    public GameObject clickedObject;
    public GameObject borkObject;

    [Header("Camera")]
    public float cameraSpeed;
    public Transform cameraTransform;
    Vector3 cameraOffset;

    [Header("Hooman")]
    public NavMeshAgent hoomanAgent;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        cameraOffset = new Vector3(cameraTransform.position.x - transform.position.x, cameraTransform.position.y - transform.position.y, cameraTransform.position.z - transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) CheckMovement();

        if (moveCamera)
        {
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, new Vector3(transform.position.x + cameraOffset.x, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z), myAgent.speed * Time.deltaTime * 0.8F);
            if (!isMoving())
            {
                moveCamera = false;
                clickedObject.SetActive(false);
            }
        }
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

        if (ableToMove && Input.GetMouseButtonDown(0))
        {
            Vector3 pos = new Vector3(UIobject.transform.position.x, UIobject.transform.position.y, UIobject.transform.position.z);
            Debug.Log(pos);

            clickedObject.transform.position = pos;
            clickedObject.SetActive(true);

            StartCoroutine(MoveTowards(pos));
        }
        else if(ableToMove && Input.GetMouseButtonDown(1))
        {
            //bork
            borkObject.SetActive(true);
            hoomanAgent.GetComponent<HoomanMover>().BorkCommand(new Vector3(UIobject.transform.position.x, UIobject.transform.position.y, UIobject.transform.position.z), borkObject);
        }
    }

    IEnumerator MoveTowards(Vector3 position)
    {
        yield return new WaitForFixedUpdate();

        myAgent.SetDestination(position);
        StartCoroutine(MoveCamera());
        //StartCoroutine(MoveHooman(position));
        hoomanAgent.GetComponent<HoomanMover>().StartMovement();
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.2F);

        moveCamera = true;
    }

    public bool isMoving()
    {
        //Debug.Log(myAgent.remainingDistance);
        return myAgent.remainingDistance > 0.1F;
    }
}
