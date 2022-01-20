using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolAreaCollider : MonoBehaviour
{
    private Rigidbody player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSchoolArea>() != null)
        {
            Debug.Log("Player entered");
            player = other.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerSchoolArea>() != null)
        {
            Debug.Log("Player exited");
            player = null;
        }
    }
    public Rigidbody GetPlayer() 
    {
        return player;
    }
}
