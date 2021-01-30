using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoInteract : MonoBehaviour
{
    ClickerHandler myMovement;

    void Start()
    {
        myMovement = GetComponent<ClickerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!myMovement.isMoving())
            {
                Dig();
            }
            else Debug.Log("I can't dig I'm moving!");
        }
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

        Debug.Log("interacting...");
    }
}
