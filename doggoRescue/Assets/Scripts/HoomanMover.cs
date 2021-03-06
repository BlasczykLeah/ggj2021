﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoomanMover : MonoBehaviour
{
    public Animator myAnim;

    public ClickerHandler doggo;
    NavMeshAgent myAgent;

    bool startMoving = false;

    public float stoppingDistance = 2F;

    bool commandMove;
    bool movingToCommand;
    GameObject borkUI;

    public bool interacting;
    public HoomanRescue carrying = null;

    public Transform armTransform;

    float speed = 0;

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
            myAgent.speed = 3F;
            myAnim.speed = 1.2F;

            if (startMoving)
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, doggo.transform.position)) > stoppingDistance * 0.8F)
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
            myAnim.speed = 1.5F;

            // stopped moving
            if (myAgent.remainingDistance < 0.1F && movingToCommand)
            {
                //check for interaction
                movingToCommand = false;
                borkUI.SetActive(false);
                Debug.Log("can I do anything here?");

                FindInderactables();
            }
            else if ((Mathf.Abs(myAgent.velocity.x) + Mathf.Abs(myAgent.velocity.z)) < 1.8F && myAgent.remainingDistance < 0.5F && movingToCommand)
            {
                myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));

                //check for interaction
                movingToCommand = false;
                borkUI.SetActive(false);
                Debug.Log("can I do anything here?");

                FindInderactables();
            }
        }

        float currSpeed = Mathf.Abs(myAgent.velocity.x) + Mathf.Abs(myAgent.velocity.z);
        myAnim.SetFloat("speed", currSpeed);
        if (currSpeed < 0.5F && currSpeed < speed) myAgent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        speed = currSpeed;
    }

    public void StartMovement()
    {
        startMoving = true;
    }

    public void BorkCommand(Vector3 position, GameObject ui)
    {
        if (carrying)
        {
            //maybe add a delay here later 
            carrying.StopCarry();
            carrying = null;
            myAnim.SetTrigger("drop");
        }
        if (!interacting)
        {
            commandMove = true;
            movingToCommand = true;
            myAgent.SetDestination(position);
            if (!borkUI) borkUI = ui;
        }
        else borkUI.SetActive(false);
    }

    public IEnumerator MoveBack(float moveTimer)
    {
        yield return new WaitForSeconds(moveTimer);
        if (!movingToCommand)
        {
            Debug.Log("moving back");
            interacting = false;
            commandMove = false;
            startMoving = true;
        }
    }

    void FindInderactables()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, 1F, LayerMask.GetMask("Interactable"));
        if (interactables.Length == 0)
        {
            Debug.Log("I didn't find anyrthing");
            StartCoroutine(MoveBack(0.5F));
            return;
        }

        Debug.Log("interacting...");
        foreach (Collider c in interactables)
        {
            if (c.GetComponent<Interactable>())
            {
                interacting = true;
                interactables[0].GetComponent<Interactable>().Interact(this, armTransform);
                return;
            }
        }


        Debug.Log("I didn't find anyrthing");
        StartCoroutine(MoveBack(0.5F));
        return;
    }
}
