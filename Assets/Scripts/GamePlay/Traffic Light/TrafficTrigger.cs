using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficTrigger : MonoBehaviour
{
    public SettingValue settingValue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.gameEvents.decreaseBehavPlayer(settingValue.decreaseValueTrafficLight);
        }
    }
}
