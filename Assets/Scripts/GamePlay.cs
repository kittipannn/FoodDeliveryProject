using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [Header("Status Player")]
    [SerializeField] private float maxBehavPlayer = 100;
    public float MaxBehavPlayer { get => maxBehavPlayer; }
    public float currentBehavPlayer;

    [Header("Time")]
    private float limitTime;
    public float LimitTime { get => limitTime; set { limitTime = value * 60;  /*Second => Minutes*/} }
    [Tooltip("Minute")]
    public float setLimit; // ไว้ test สำหรับ ดูว่าถ้าเอาไปใส่ที่อื่นจะได้ไหม
    private bool timerIsRunning = false;

    void Start()
    {
        //Events
        GameEvents.gameEvents.onDecreaseBehavPlayer += decreaseBehav;
        GameEvents.gameEvents.onStartGame += playerStartGame;
        GameEvents.gameEvents.onGameOver += GameOver;
        //Setting
        currentBehavPlayer = PlayerPrefs.GetFloat("BehavPlayer", maxBehavPlayer);
        LimitTime = setLimit;
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (timerIsRunning) //เริ่มนับเวลา
            countTime();
    }
    //BehavStat
    private void decreaseBehav(float decreaseValue)
    {
        currentBehavPlayer -= decreaseValue;
        setPlayerPrefsBehavStatus();
        GameEvents.gameEvents.UpdateStatusPlayer();
        if (currentBehavPlayer <= 0)
        {
            currentBehavPlayer = 0;
            GameEvents.gameEvents.gameOver();
        }
    }
    private void increaseBehav(float increaseValue) //ทำไว้เผื่อต้องการทำระบบที่สามารถเพิ่มค่าประพฤติได้
    {
        currentBehavPlayer += increaseValue;
        setPlayerPrefsBehavStatus();
        GameEvents.gameEvents.UpdateStatusPlayer();
    }
    private void setPlayerPrefsBehavStatus() 
    {
        PlayerPrefs.SetFloat("BehavPlayer", currentBehavPlayer);
        PlayerPrefs.Save();
    }


    //gameOver
    private void GameOver() 
    {
        Debug.Log("GameOver");
        //-------------------------------------------------------------------------------------- Test -----------------------------------------------------------------------------------------------
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<PlayerDemo>().enabled = false;
        //-------------------------------------------------------------------------------------- Test -----------------------------------------------------------------------------------------------

    }
    //TIme In Game
    private void countTime() 
    {
        if (limitTime <= 0) // ถ้าเวลาหมดแล้วจะทำอะไรสักอย่าง
        {
            limitTime = 0;
            timerIsRunning = false;
            Debug.Log("Game Over");
        }
        else
        {
            limitTime -= Time.deltaTime;
        }
    }
    private void playerStartGame()
    {
        timerIsRunning = true;
        GameEvents.gameEvents.UpdateStatusPlayer();
        //-------------------------------------------------------------------------------------- Test -----------------------------------------------------------------------------------------------
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDemo>().enabled = true;
        //-------------------------------------------------------------------------------------- Test -----------------------------------------------------------------------------------------------
    }
}
