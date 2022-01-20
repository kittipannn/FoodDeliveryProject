using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents gameEvents;
    private void Awake()
    {
        gameEvents = this;
    }
    public event Action<float> onDecreaseBehavPlayer;
    public void decreaseBehavPlayer(float value)
    {
        if (onDecreaseBehavPlayer != null)
        {
            onDecreaseBehavPlayer(value);
        }
    }
    public event Action<float> onIncreaseBehavPlayer; //���͵�ͧ���������Ҥ�����оĵ�
    public void increaseBehavPlayer(float value)
    {
        if (onIncreaseBehavPlayer != null)
        {
            onIncreaseBehavPlayer(value);
        }
    }

    public event Action onStartTitleGame; // ������������
    public void startGameBtn()
    {
        if (onStartTitleGame != null)
        {
            onStartTitleGame();
        }
    }
    public event Action<int> oncheckEvents; //checkevent in scenes
    public void checkEvents(int valus) 
    {
        if (oncheckEvents != null)
        {
            oncheckEvents(valus);
        }
    }
    public event Action onStartGame; // ������������
    public void startGame()
    {
        if (onStartGame != null)
        {
            onStartGame();
        }
    }
    public event Action onFinishGame; // when player moves to finishPoint
    public void finishGame() 
    {
        if (onFinishGame != null)
        {
            onFinishGame();
        }
    }
    public event Action onGameOver;
    public void gameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public event Action onCrosswalkTrigger;
    public void CrossWalkTrigger()
    {
        if (onCrosswalkTrigger != null)
        {
            onCrosswalkTrigger();
        }
    }
    // UI
    public event Action onUpdateStatusPlayer;
    public void UpdateStatusPlayer()
    {
        if (onUpdateStatusPlayer != null)
        {
            onUpdateStatusPlayer();
        }
    }

}
