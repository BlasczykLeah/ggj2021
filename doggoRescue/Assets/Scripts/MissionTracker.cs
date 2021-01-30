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
}
