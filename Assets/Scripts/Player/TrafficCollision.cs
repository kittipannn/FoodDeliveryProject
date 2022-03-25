using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEvents.gameEvents.gameOver();
        }
    }
}
