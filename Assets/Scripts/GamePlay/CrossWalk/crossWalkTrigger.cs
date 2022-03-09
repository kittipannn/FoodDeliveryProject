using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossWalkTrigger : MonoBehaviour
{
    [SerializeField] crossWalkScript[] crossingPeople;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enable Script CrossWalk");
            GameObject.FindObjectOfType<CrossWalkCollider>().enabled = true;
            GameObject.FindObjectOfType<StopCollider>().enabled = true;
            foreach (var item in crossingPeople)
            {
                item.enabled = true;
            }
        }
    }
    public int GetNumberOfPeople()
    {
        return crossingPeople.Length;
    }
}

