using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficTrigger : MonoBehaviour
{
    public SettingValue settingValue;
    public int NumOfEvent;
    bool checkEvent = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !checkEvent) 
        {
            checkEvent = true;
            GameEvents.gameEvents.decreaseBehavPlayer(settingValue.decreaseValueTrafficLight);
            GameEvents.gameEvents.checkEvents(NumOfEvent);
        }
    }
}
