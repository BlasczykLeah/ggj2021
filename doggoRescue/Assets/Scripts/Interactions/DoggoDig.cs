using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoDig : Interactable
{
    public override void Interact(ClickerHandler doggo)
    {
        Debug.Log(doggo.gameObject.name + " has interacted with " + name, gameObject);
    }
}
