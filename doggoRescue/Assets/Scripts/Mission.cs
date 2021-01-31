using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Mission
{
    //This script is in charge of holding each of the mission components

    public GameObject person, physicalHints, scentTrail;
    public Sprite sign;
    public Vector3 personLocation;
    public string personDirection, missionDescription;
}
