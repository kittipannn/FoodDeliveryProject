using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCollider : MonoBehaviour
{
    List<crossWalkScript> crossWalkScriptList;
    [SerializeField] crossWalkTrigger crossWalkTrigger;
    [SerializeField] GameObject crossWalkColli;
    int numOfPeople;
    private void Start()
    {
        this.enabled = false;
    }
    private void OnEnable()
    {
        numOfPeople = crossWalkTrigger.GetNumberOfPeople();
        crossWalkScriptList = new List<crossWalkScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("People"))
        {
            crossWalkScriptList.Add(other.gameObject.GetComponent<crossWalkScript>());
        }
    }
    private void Update()
    {
        if (crossWalkScriptList != null)
        {
            if (crossWalkScriptList.Count == numOfPeople)
            {
                crossWalkColli.SetActive(false);
                this.enabled = false;
            }
        }
    }
}
