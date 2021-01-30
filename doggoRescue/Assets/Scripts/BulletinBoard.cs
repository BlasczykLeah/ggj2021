using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinBoard : MonoBehaviour
{
    //This script will be holding and assigning missions to doggo

    public List<Mission> missionList = new List<Mission>();
    public GameObject map;
    public Button mission1, mission2, mission3, mission4;
    public int missionNumber = 0;
    public bool missionStarted = false, mapOpen = false, inRange = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange && !mapOpen)
        {
            OpenMap();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mapOpen)
        {
            CloseMap();
        }

        switch (missionNumber)
        {
            case 0:
                mission1.interactable = true;
                mission2.interactable = false;
                mission3.interactable = false;
                mission4.interactable = false;
                return;
            case 1:
                mission1.enabled = false;
                return;
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

    public void OpenMap()
    {
        map.SetActive(true);
        mapOpen = true;
    }

    public void CloseMap()
    {
        map.SetActive(false);
        mapOpen = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Doggo"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Doggo"))
        {
            inRange = false;
        }
    }
}
