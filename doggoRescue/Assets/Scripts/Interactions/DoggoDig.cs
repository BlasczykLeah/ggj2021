using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoDig : Interactable
{
    public Animator myAnim;

    public float resetTime;

    bool resetting = false;

    public override void Interact(ClickerHandler doggo)
    {
        if (!resetting)
        {
            resetting = true;
            Debug.Log(doggo.gameObject.name + " digging", gameObject);
            myAnim.SetTrigger("dig");
            doggo.myAnim.SetTrigger("dig");
            StartCoroutine(SnowGrow());
        }
    }

    IEnumerator SnowGrow()
    {
        yield return new WaitForSeconds(resetTime);
        myAnim.SetTrigger("reset");

        yield return new WaitForSeconds(2F);
        resetting = false;
    }
}
