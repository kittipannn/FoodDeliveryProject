using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<SphereController>().enabled = false;
            SoundManager.soundInstance.Play("Win");
            SoundManager.soundInstance.Pause("BGM");
            GameEvents.gameEvents.finishGame();
        }
    }
}
