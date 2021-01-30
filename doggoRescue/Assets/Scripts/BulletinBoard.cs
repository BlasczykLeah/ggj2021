using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinBoard : MonoBehaviour
{
    //This script will be holding and assigning missions to doggo

    public List<Mission> missionList = new List<Mission>();
    public int missionNumber = 0;
    public bool missionStarted = false;
    public GameObject map;
    public bool mapOpen = false;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space) && !missionStarted)
        {
            AssignMission();
        }
        else if (Input.GetKeyDown(KeyCode.P) && missionStarted)
        {
            ReturnMission();
        }
        */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void AssignMission()
    {
        missionStarted = true;
        MissionTracker.inst.currentMission = missionList[missionNumber];
        missionNumber++;
    }

    public void ReturnMission()
    {
        missionStarted = false;
        MissionTracker.inst.currentMission = new Mission();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Doggo"))
        {
            inRange = true;
        }
    }
}
