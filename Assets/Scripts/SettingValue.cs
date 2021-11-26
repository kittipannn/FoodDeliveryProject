using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueEventInGame", menuName = "Scriptable SettingValue")]
public class SettingValue : ScriptableObject
{


    [Header("Crosswalk Setting")]
    public float decreaseValueCrossWalk;

    [Header("School District Setting")]
    public float limitedSpeedInSchoolArea;
    public float decreaseValueSchoolArea;
    public float decreaseTimeSchoolArea;

    [Header("Traffic Light Setting")]
    public float decreaseValueTrafficLight;
}
