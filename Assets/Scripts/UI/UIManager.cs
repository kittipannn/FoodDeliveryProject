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
    [SerializeField] GameObject Player;



    [Header("FinishPanel")]
    [SerializeField] CheckEvents checkEvents;
    [SerializeField] GameObject finishPanel;
    [SerializeField] List<Image> symbolInEventImg;
    [SerializeField] List<Sprite> symbol;
    [SerializeField] List<TextMeshProUGUI> descriptionText;

    [Header("TurnLight")]
    [SerializeField] Sprite[] ImgTurnlight;
    [SerializeField] Image[] Turnlight;
    bool LightOn = true;

    void Start()
    {
        //Events
        GameEvents.gameEvents.onUpdateStatusPlayer += updateStatusPlayer;
        GameEvents.gameEvents.onStartGame += (() => showTimeText = true);
        GameEvents.gameEvents.onFinishGame += OnShowFinishPanel;

        //Setting
        BehavSlider.maxValue = gamePlay.MaxBehavPlayer;
        BehavSlider.value = 100 - gamePlay.currentBehavPlayer;
        //BehavSlider.value =  gamePlay.currentBehavPlayer;


        //motorcycle = GameObject.FindGameObjectWithTag("Player").GetComponent<MotorcycleControl>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {

        timeText.text = displayTimer();
        speedText.text = displaySpeed(Player.GetComponent<SphereController>().PlayerSpeed);
        //if (showTimeText)
        //    timeText.text = displayTimer();
    }
    private void updateStatusPlayer() //Ŵ����ͼ����蹾ĵԾĵԡ�������
    {
        BehavSlider.value = 100 - gamePlay.currentBehavPlayer;
        //BehavSlider.value =  gamePlay.currentBehavPlayer;
    }

    string displayTimer()
    {
        float timer = gamePlay.LimitTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);
        string showtime = string.Format("{0:00}   {1:00}", minutes, seconds);
        return showtime;
    }

    string displaySpeed(float speedPlayer) 
    {
        int speed = Mathf.RoundToInt(speedPlayer) / 10; 
        return speed.ToString();
    }
    private void OnShowFinishPanel() 
    {
        finishPanel.SetActive(true);
        for (int i = 0; i < checkEvents.eventInScenes.Count; i++)
        {
            if (!checkEvents.eventInScenes[i].eventCheck)
            {
                symbolInEventImg[i].overrideSprite = symbol[0];
            }
            else
            {
                symbolInEventImg[i].overrideSprite = symbol[1];
            }
            descriptionText[i].text = checkEvents.eventInScenes[i].eventDetails;
        }

    }


  
    public void changeImgTurnlight(int i)
    {
        if (i == 1) // left
        {
            Turnlight[1].sprite = ImgTurnlight[1];
            if (LightOn)
            {
                Turnlight[0].sprite = ImgTurnlight[2];
                LightOn = false;
            }
            else
            {
                Turnlight[0].sprite = ImgTurnlight[3];
                LightOn = true;
            }
        }
        else if (i == 2)// right
        {
            Turnlight[0].sprite = ImgTurnlight[0];
            if (LightOn)
            {
                Turnlight[1].sprite = ImgTurnlight[4];
                LightOn = false;
            }
            else
            {
                Turnlight[1].sprite = ImgTurnlight[5];
                LightOn = true;
            }
        }
        else
        {
            LightOn = false;
            Turnlight[0].sprite = ImgTurnlight[0];
            Turnlight[1].sprite = ImgTurnlight[1];
            
        }
    }
}
