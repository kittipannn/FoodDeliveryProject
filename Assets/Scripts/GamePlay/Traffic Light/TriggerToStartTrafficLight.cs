using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToStartTrafficLight : MonoBehaviour
{
    [SerializeField] TrafficLight trafficLight;
    private void Start()
    {
        trafficLight = gameObject.GetComponentInParent<TrafficLight>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trafficLight.enabled = true;
        }
    }
}
