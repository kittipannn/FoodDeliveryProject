using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnButtonManager : MonoBehaviour
{
    [SerializeField] GamePlay gamePlay;

    [SerializeField] Button restartOver;
    [SerializeField] Button BacktoMainMenuOver;
    [SerializeField] Button restartOption;
    [SerializeField] Button BacktoMainMenuOption;
    [SerializeField] Button BacktoMainMenuFinish;

    private void Start()
    {
        restartOver.onClick.AddListener(Onrestart);
        BacktoMainMenuOver.onClick.AddListener(OnBacnkToMainMenu);

        restartOption.onClick.AddListener(Onrestart);
        BacktoMainMenuOption.onClick.AddListener(OnBacnkToMainMenu);
        BacktoMainMenuFinish.onClick.AddListener(OnBacnkToMainMenu);
    }
    void Onrestart() 
    {
        gamePlay.restartGame();
    }
    void OnBacnkToMainMenu()
    {
        gamePlay.backToMain();
    }
}
