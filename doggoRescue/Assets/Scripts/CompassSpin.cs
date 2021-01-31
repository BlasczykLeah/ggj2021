using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassSpin : MonoBehaviour
{
    public Transform playerLocation;
    public RectTransform arrow;

    private void Update()
    {

        Vector3 direction = playerLocation.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);

        float angle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);
        arrow.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator UpdateCompass()
    {
        yield return new WaitForSeconds(2F);

        float zDiff = playerLocation.position.z - transform.position.z;
        float xDiff = playerLocation.position.x - transform.position.x;

        float angle = Mathf.Tan(zDiff / xDiff);

        //arrow.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * angle));
        arrow.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * angle));

        StartCoroutine(UpdateCompass());
    }
}
