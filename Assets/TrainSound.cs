using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSound : MonoBehaviour
{
    [SerializeField] AudioSource alarm;
    [SerializeField] AudioSource train;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alarm.Play();
            train.PlayDelayed(1);
        }
    }
}
