using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWalkCollider : MonoBehaviour
{
    public SettingValue settingValue;
    public int NumOfEvent;
    bool checkEvent = false;
    private void Start()
    {
        this.enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnEnable()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !checkEvent)
        {
            Debug.Log("Please stop at the crosswalk");
            checkEvent = true;
            float valueToDecrease = settingValue.decreaseValueCrossWalk;
            GameEvents.gameEvents.decreaseBehavPlayer(valueToDecrease);
            GameEvents.gameEvents.checkEvents(NumOfEvent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
