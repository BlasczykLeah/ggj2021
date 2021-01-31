using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSmell : MonoBehaviour
{
    public List<Animator> scents;

    public void Smell()
    {
        foreach (Animator a in scents) a.SetTrigger("fade");
    }
}
