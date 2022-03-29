using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossWalkTrigger : MonoBehaviour
{
    [SerializeField] crossWalkScript[] crossingPeople;
    [SerializeField] CrossWalkCollider crosswalkColli;
    [SerializeField] StopCollider stopCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enable Script CrossWalk");
            crosswalkColli.enabled = true;
            stopCollider.enabled = true;
            foreach (var item in crossingPeople)
            {
                item.enabled = true;
            }
            this.gameObject.SetActive(false);
        }

    }
    public int GetNumberOfPeople()
    {
        return crossingPeople.Length;
    }
}

