using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoomanRescue : Interactable
{
    public bool carrying;

    public Vector3 snapPoint = new Vector3(-0.03F, 0.16F, 0.62F);

    public override void Interact(HoomanMover hooman)
    {
        if (!carrying)
        {
            // set hooman animation and maybe location
            Debug.Log("helping fello hooman!");
            hooman.carrying = this;
            hooman.myAnim.SetTrigger("pickup");

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = Quaternion.identity;
            transform.rotation = hooman.transform.rotation;
            transform.SetParent(hooman.transform);
            transform.localPosition = snapPoint;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            carrying = true;

            ResetHooman(hooman, 2F);
        }
    }

    public void StopCarry()
    {
        Debug.Log("putting him down");
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        carrying = false;
    }

    public void EndCarry()
    {
        transform.parent.GetComponent<HoomanMover>().carrying = null;
        StopCarry();
    }
}
