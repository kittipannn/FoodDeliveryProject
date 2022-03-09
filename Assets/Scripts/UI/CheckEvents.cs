using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class eventInScene
{
    public string eventName;
    public string eventDetails;
    public bool eventCheck = true;
}

public class CheckEvents : MonoBehaviour
{
    public static CheckEvents checkEvents;
    public List<eventInScene> eventInScenes;
    void Start()
    {
        GameEvents.gameEvents.oncheckEvents += checkEventsInScenes;
        for (int i = 0; i < eventInScenes.Count; i++)
        {
            eventInScenes[i].eventCheck = true;
        }
    }

    void checkEventsInScenes(int numofEvent)
    {
        for (int i = 0; i < eventInScenes.Count; i++)
        {
            if (i == numofEvent-1)
            {
                if (eventInScenes[i].eventCheck == true)
                {
                    eventInScenes[i].eventCheck = false;
                }
                break;
            }
        }
    }
}
