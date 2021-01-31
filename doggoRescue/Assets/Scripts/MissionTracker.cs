using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTracker : MonoBehaviour
{
    //This goes on doggo

    public static MissionTracker inst;

    public Mission currentMission;

    private void Start()
    {
        inst = this;
    }

    public void SmellTrail()
    {
        Debug.Log("sniff sniff");
        AudioManager.inst.PlaySniff();

        if (currentMission.scentTrail)
            currentMission.scentTrail.GetComponent<ShowSmell>().Smell();
    }
}
