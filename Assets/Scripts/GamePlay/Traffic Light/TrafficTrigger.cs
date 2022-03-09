using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficTrigger : MonoBehaviour
{
    public SettingValue settingValue;
    public int NumOfEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.gameEvents.decreaseBehavPlayer(settingValue.decreaseValueTrafficLight);
            GameEvents.gameEvents.checkEvents(NumOfEvent);
        }
    }
}
