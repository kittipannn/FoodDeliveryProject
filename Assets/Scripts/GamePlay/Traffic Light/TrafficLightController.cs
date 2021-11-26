using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    [SerializeField] float timeTakenBetweenTrafficLight;
    float timefortrafficlight = 0;
    int currentTrafficLight = 0;
    int previoustrafficlight = 0;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        trafficLight();
    }
    void trafficLight()
    {
        if (Time.time >= timeTakenBetweenTrafficLight)
        {
            timefortrafficlight += timeTakenBetweenTrafficLight;
            this.gameObject.transform.GetChild(currentTrafficLight).gameObject.SetActive(false);
            if (previoustrafficlight != currentTrafficLight)
                this.gameObject.transform.GetChild(previoustrafficlight).gameObject.SetActive(true);

            previoustrafficlight = currentTrafficLight;
            if (currentTrafficLight >= transform.childCount - 1)
                currentTrafficLight = 0;
            else
                currentTrafficLight += 1;
        }
        Debug.Log(Time.time);
    }
}
