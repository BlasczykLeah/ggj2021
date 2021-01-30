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

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
                if(!doggo.isMoving() && Mathf.Abs(Vector3.Distance(transform.position, doggo.transform.position)) < stoppingDistance)
                {
                    myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                    startMoving = false;
                }
            }
            else myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }
    }

    public void StartMovement()
    {
        startMoving = true;
    }
}
