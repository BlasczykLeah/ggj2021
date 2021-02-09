using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpin : MonoBehaviour
{
    public static SpellSpin spinner;

    public Transform playerLocation;
    public Transform personLocation;
    public RectTransform arrow;

    public Animator myFader;

    private void Awake()
    {
        spinner = this;
    }
    private void Update()
    {
        if (personLocation)
        {
            Vector3 direction = playerLocation.transform.position - personLocation.position;
            direction = new Vector3(direction.x, 0, direction.z);

            float angle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);
            arrow.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void ShowArrow()
    {
        myFader.SetTrigger("fade");
    }
}
