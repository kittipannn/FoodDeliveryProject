using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnButtonManager : MonoBehaviour
{
    [SerializeField] Button startGame;
    private void Start()
    {
        //startGame.onClick.AddListener(() => GameEvents.gameEvents.startGameBtn());
        //startGame.onClick.AddListener(() => startGame.interactable = false);
    }
}
