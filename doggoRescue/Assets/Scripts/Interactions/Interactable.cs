using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(HoomanMover hooman)
    {
        Debug.Log(hooman.gameObject.name + " has interacted with " + name, gameObject);
        ResetHooman(hooman, 1F);
    }

    public virtual void Interact(DoggoInteract doggo)
    {
        Debug.Log(doggo.gameObject.name + " has interacted with " + name, gameObject);
    }

    protected void ResetHooman(HoomanMover hooman, float time)
    {
        StartCoroutine(hooman.MoveBack(time));
    }
}
