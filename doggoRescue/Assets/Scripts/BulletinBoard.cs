using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinBoard : MonoBehaviour
{
    //This script will be holding and assigning missions to doggo

    public static BulletinBoard inst;

    public List<Mission> missionList = new List<Mission>();
    public GameObject map;
    public Button mission1, mission2, mission3, mission4;
    public int missionNumber = 0;
    public bool missionStarted = false, mapOpen = false, inRange = false;

    private void Start()
    {
        inst = this;
    }

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
    }

    public void AssignMission()
    {
        missionStarted = true;
        MissionTracker.inst.currentMission = missionList[missionNumber];
        MissionTracker.inst.currentMission.person.SetActive(true);
        MissionTracker.inst.currentMission.physicalHints.SetActive(true);
        CloseMap();
    }

    public void ReturnMission()
    {
        missionNumber++;
        missionStarted = false;
        MissionTracker.inst.currentMission.person.GetComponent<HoomanRescue>().EndCarry();
        MissionTracker.inst.currentMission.person.SetActive(false);
        MissionTracker.inst.currentMission.physicalHints.SetActive(false);
        MissionTracker.inst.currentMission = new Mission();
    }

    public void OpenMap()
    {
        UpdateMap();
        map.SetActive(true);
        mapOpen = true;
        ClickerHandler.inst.canMove = false;
    }

    public void CloseMap()
    {
        map.SetActive(false);
        mapOpen = false;
        ClickerHandler.inst.canMove = true;
    }

    public void UpdateMap()
    {
        switch (missionNumber)
        {
            case 0:
                mission1.interactable = true;
                mission2.interactable = false;
                mission3.interactable = false;
                mission4.interactable = false;
                return;
            case 1:
                mission1.gameObject.SetActive(false);
                mission2.interactable = true;
                mission3.interactable = false;
                mission4.interactable = false;
                return;
            case 2:
                mission1.gameObject.SetActive(false);
                mission2.gameObject.SetActive(false);
                mission3.interactable = true;
                mission4.interactable = false;
                return;
            case 3:
                mission1.gameObject.SetActive(false);
                mission2.gameObject.SetActive(false);
                mission3.gameObject.SetActive(false);
                mission4.interactable = true;
                return;
            default:
                mission1.gameObject.SetActive(false);
                mission2.gameObject.SetActive(false);
                mission3.gameObject.SetActive(false);
                mission4.gameObject.SetActive(false);
                return;
        }
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
