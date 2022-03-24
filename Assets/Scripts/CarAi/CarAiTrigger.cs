using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAiTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Car;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Car.gameObject.GetComponent<CarAiNavMesh>().AiMove();
            Debug.Log("Trigger Start");
        }
    }
}
