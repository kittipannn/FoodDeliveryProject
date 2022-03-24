using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnButtonManager : MonoBehaviour
{
    [SerializeField] GamePlay gamePlay;

    [SerializeField] Button restart;
    [SerializeField] Button BacktoMainMenu;
    
    private void Start()
    {
        restart.onClick.AddListener(Onrestart);
        BacktoMainMenu.onClick.AddListener(OnBacnkToMainMenu);
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
