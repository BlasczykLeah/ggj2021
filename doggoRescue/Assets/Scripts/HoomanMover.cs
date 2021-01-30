using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoomanMover : MonoBehaviour
{
    public ClickerHandler doggo;
    NavMeshAgent myAgent;

    bool startMoving = false;

    public float stoppingDistance = 2F;

    bool commandMove;
    bool movingToCommand;
    GameObject borkUI;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!commandMove)
        {
            myAgent.speed = 2F;

            if (doggo.isMoving())
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, doggo.transform.position)) > 3F)
                    myAgent.SetDestination(new Vector3(doggo.transform.position.x, transform.position.y, doggo.transform.position.z));
                else myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            }
            else myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));

            if (startMoving)
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, doggo.transform.position)) > stoppingDistance * 2F || !doggo.isMoving())
                {
                    myAgent.SetDestination(new Vector3(doggo.transform.position.x, transform.position.y, doggo.transform.position.z));
                    if (!doggo.isMoving() && Mathf.Abs(Vector3.Distance(transform.position, doggo.transform.position)) < stoppingDistance)
                    {
                        myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                        startMoving = false;
                    }
                }
                else myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            }
        }
        else
        {
            myAgent.speed = 5F;

            // stopped moving
            if (myAgent.remainingDistance < 0.1F && movingToCommand)
            {
                //check for interaction
                movingToCommand = false;
                borkUI.SetActive(false);
                Debug.Log("can I do anything here?");
                StartCoroutine(MoveBack());
            }
        }
    }

    public void StartMovement()
    {
        startMoving = true;
    }

    public void BorkCommand(Vector3 position, GameObject ui)
    {
        commandMove = true;
        movingToCommand = true;
        myAgent.SetDestination(position);
        if (!borkUI) borkUI = ui;
    }

    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(0.5F);
        Debug.Log("I didn't find anyrthing, moving back");
        commandMove = false;
        startMoving = true;
    }
}
