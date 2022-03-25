using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolArea : MonoBehaviour
{
    public SettingValue settingValue;
    float time;
    float decreaseTime;
    float decreaseValue;
    float limitedSpeedInSchoolArea = 10;
    SchoolAreaCollider schoolArea;

    public int NumOfEvent;
    bool enableEvent = false;

    bool checkEvent = false;
    void Start()
    {
        schoolArea = gameObject.GetComponentInChildren<SchoolAreaCollider>();
        //setting
        decreaseValue = settingValue.decreaseValueSchoolArea;
        decreaseTime = settingValue.decreaseTimeSchoolArea;
        limitedSpeedInSchoolArea = settingValue.limitedSpeedInSchoolArea;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (schoolArea.GetPlayer() != null)
        {
            if (schoolArea.GetPlayer().velocity.magnitude > limitedSpeedInSchoolArea && !checkEvent)
            {
                GameEvents.gameEvents.decreaseBehavPlayer(decreaseValue);
                checkEvent = true;
                //time += Time.deltaTime;
                //if (time >= decreaseTime)
                //{
                //    time = 0;
                //    GameEvents.gameEvents.decreaseBehavPlayer(decreaseValue);
                //}
                if (!enableEvent)
                {
                    enableEvent = true;
                    GameEvents.gameEvents.checkEvents(NumOfEvent);
                }
            }
        }
    }
}
