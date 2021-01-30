using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(HoomanMover hooman)
    {
        Debug.Log(hooman.gameObject.name + " has interacted with " + name);
        ResetHooman(hooman, 1F);
    }

   void ResetHooman(HoomanMover hooman, float time)
    {
        StartCoroutine(hooman.MoveBack(time));
    }
}
