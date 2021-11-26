using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GamePlay gamePlay;
    [Header("Status Player")]
    [SerializeField] Slider BehavSlider;
    [SerializeField] float testValue;

    [Header("Time")]
    [SerializeField] TMP_Text timeText;
    private bool showTimeText = false;

    [Header("Speedometer")]
    [SerializeField] TMP_Text speedText;
    MotorcycleControl motorcycle;
    GameObject playerTest;
    
    void Start()
    {
        //Events
        GameEvents.gameEvents.onUpdateStatusPlayer += updateStatusPlayer;
        GameEvents.gameEvents.onStartGame += (() => showTimeText = true);

        //Setting
        BehavSlider.maxValue = gamePlay.MaxBehavPlayer;
        BehavSlider.value = gamePlay.currentBehavPlayer;
        //motorcycle = GameObject.FindGameObjectWithTag("Player").GetComponent<MotorcycleControl>();
        playerTest = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        timeText.text = displayTimer();
        speedText.text = displaySpeed(playerTest.GetComponent<Rigidbody>().velocity.magnitude);
        //if (showTimeText)
        //    timeText.text = displayTimer();
    }
    private void updateStatusPlayer() //ลดเมื่อผู้เล่นพฤติพฤติกรรมไม่ดี
    {
        BehavSlider.value = gamePlay.currentBehavPlayer;
    }

    string displayTimer()
    {
        float timer = gamePlay.LimitTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);
        string showtime = string.Format("{0:00} : {1:00}", minutes, seconds);
        return showtime;
    }

    string displaySpeed(float speedPlayer) 
    {
        int speed = Mathf.RoundToInt(speedPlayer);
        return speed.ToString();
    }

}
