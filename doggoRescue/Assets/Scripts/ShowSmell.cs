using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSmell : MonoBehaviour
{
    public void Smell()
    {
        foreach (Animator a in GetComponentsInChildren<Animator>()) a.SetTrigger("fade");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) Smell();
    }
}
