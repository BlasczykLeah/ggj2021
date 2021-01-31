using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoDig : MonoBehaviour
{
    public Animator myAnim;

    public float resetTime;
    public float riseTime;

    bool resetting = false;

    public bool Interact(ClickerHandler doggo)
    {
        if (!resetting)
        {
            resetting = true;
            Debug.Log(doggo.gameObject.name + " digging", gameObject);

            if (doggo.isMoving())
                StartCoroutine(doggo.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z)));

            myAnim.SetTrigger("dig");
            doggo.myAnim.SetTrigger("dig");
            StartCoroutine(SnowGrow());
            return true;
        }
        return false;
    }

    IEnumerator SnowGrow()
    {
        yield return new WaitForSeconds(resetTime);
        myAnim.SetTrigger("reset");

        yield return new WaitForSeconds(riseTime);
        resetting = false;
    }
}
