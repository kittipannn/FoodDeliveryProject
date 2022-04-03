using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] CheckEvents checkEvents;
    SceneLoader sceneLoader;
    [Header("Status Player")]
    [SerializeField] private float maxBehavPlayer;
    public float MaxBehavPlayer { get => maxBehavPlayer; }
    public float currentBehavPlayer;
    GameObject Player;

    [Header("Time")]
    private float limitTime;
    public float LimitTime { get => limitTime; set { limitTime = value * 60;  /*Second => Minutes*/} }
    [Tooltip("Minute")]
    public float setLimit; // ไว้ test สำหรับ ดูว่าถ้าเอาไปใส่ที่อื่นจะได้ไหม
    private bool timerIsRunning = false;

    bool Over = false;
    private void Awake()
    {
        maxBehavPlayer = checkEvents.eventInScenes.Count;
        //currentBehavPlayer = PlayerPrefs.GetFloat("BehavPlayer", maxBehavPlayer);
        currentBehavPlayer = maxBehavPlayer;
        sceneLoader = gameObject.GetComponent<SceneLoader>();
    }
    void Start()
    {
        //Events
        GameEvents.gameEvents.onDecreaseBehavPlayer += decreaseBehav;
        GameEvents.gameEvents.onStartGame += playerStartGame;
        GameEvents.gameEvents.onGameOver += GameOver;

        //Setting

        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<SphereController>().enabled = false;
        LimitTime = setLimit;
    }
    private void Update()
    {
        if (timerIsRunning) //เริ่มนับเวลา
            countTime();
    }
    //BehavStat
    private void decreaseBehav(float decreaseValue)
    {
        CameraShake.Instance.shakeCamera(1f, 0.1f);
        SoundManager.soundInstance.Play("DecreaseBehav");
        currentBehavPlayer -= decreaseValue;
        //setPlayerPrefsBehavStatus();
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
        //setPlayerPrefsBehavStatus();
        GameEvents.gameEvents.UpdateStatusPlayer();
    }
    //private void setPlayerPrefsBehavStatus()
    //{
    //    PlayerPrefs.SetFloat("BehavPlayer", currentBehavPlayer);
    //    PlayerPrefs.Save();
    //}
    //gameOver
    private void GameOver() 
    {
        
        Debug.Log("GameOver");
        Player.GetComponent<SphereController>().enabled = false;
        currentBehavPlayer = maxBehavPlayer;
        if (!Over)
        {
            Over = true;
            SoundManager.soundInstance.Play("Lose");
            SoundManager.soundInstance.Pause("BGM");
        }
        //setPlayerPrefsBehavStatus();

    }
    public void restartGame() // set Behav when restartgame
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneLoader.LoadingScene(sceneName);
    }
    public void backToMain() 
    {
        sceneLoader.LoadingScene("MenuScene");
    }
    //TIme In Game
    private void countTime() 
    {
        if (limitTime <= 0) // ถ้าเวลาหมดแล้วจะทำอะไรสักอย่าง
        {
            limitTime = 0;
            timerIsRunning = false;
            GameEvents.gameEvents.gameOver();
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
        Player.GetComponent<SphereController>().enabled = true;
        //-------------------------------------------------------------------------------------- Test -----------------------------------------------------------------------------------------------
    }

}
