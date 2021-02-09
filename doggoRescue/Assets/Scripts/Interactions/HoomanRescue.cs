using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoomanRescue : Interactable
{
    public bool carrying;

    public Vector3 snapPoint = new Vector3(-0.03F, 0.16F, 0.62F);

    HoomanMover rescuer;

    public override void Interact(HoomanMover hooman, Transform location)
    {
        if (!carrying)
        {
            // set hooman animation and maybe location
            Debug.Log("helping fello hooman!");
            rescuer = hooman;
            hooman.carrying = this;
            hooman.myAnim.SetTrigger("pickup");

            StartCoroutine(DelayPickup(location));
            
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
        rescuer.myAnim.SetTrigger("drop");
        rescuer.carrying = null;
        StopCarry();
    }

    IEnumerator DelayPickup(Transform transforme)
    {
        yield return new WaitForSeconds(1.5F);

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = Quaternion.Euler(-90, 0, rescuer.transform.rotation.eulerAngles.y + 90);
        //transform.rotation = hooman.transform.rotation;
        transform.SetParent(transforme);
        transform.localPosition = snapPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && !carrying && !GetComponent<Rigidbody>().isKinematic)
            GetComponent<Rigidbody>().isKinematic = true;
    }
}
