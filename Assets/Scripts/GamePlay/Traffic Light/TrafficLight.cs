using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    float timeCount;
    int timeToChangeLight;
    int currentTrafficLight;
    [SerializeField] int greenTime = 10;
    [SerializeField] int yellowTime = 3;
    [SerializeField] int redTime = 7;
    [SerializeField] GameObject triggerTraffic;
    public List<GameObject> test;
    void Start()
    {
        foreach (var gameObject in test)
        {
            gameObject.SetActive(false);
        }
        currentTrafficLight = 0;
        setTrafficLight();
        setCollider();
    }
    //private void OnEnable()
    //{
    //    timeCount = 3;
    //    currentTrafficLight = 0;
    //    setTrafficLight();
    //}

    // Update is called once per frame
    void Update()
    {
        changeLight();
    }
    void changeLight() 
    {
        setTimeToChange();
        if (timeCount >= timeToChangeLight)
        {
            timeCount = 0;
            currentTrafficLight++;
            if (currentTrafficLight > 2)
                currentTrafficLight = 0;
            setTrafficLight();
            setCollider();
        }
        else
        {
            timeCount += Time.deltaTime;
        }

    }
    void setTimeToChange()
    {
        if (currentTrafficLight == 0)
        {
            timeToChangeLight = greenTime;
        }
        else if (currentTrafficLight == 1)
        {
            timeToChangeLight = yellowTime;
        }
        else if (currentTrafficLight == 2)
        {
            timeToChangeLight = redTime;
        }

    }
    void setTrafficLight() 
    {
        foreach (var item in test)
        {
            item.SetActive(false);
        }
        test[currentTrafficLight].SetActive(true);
    }
    void setCollider() 
    {
        if (currentTrafficLight == 0)
        {
            triggerTraffic.SetActive(false);
        }
        else if (currentTrafficLight == 1)
        {
            triggerTraffic.SetActive(true);
        }
        else if (currentTrafficLight == 2)
        {
            triggerTraffic.SetActive(true);
        }
    }

}
