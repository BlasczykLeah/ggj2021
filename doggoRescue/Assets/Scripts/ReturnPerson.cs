using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPerson : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mission"))
        {
            BulletinBoard.inst.ReturnMission();
        }
    }
}
