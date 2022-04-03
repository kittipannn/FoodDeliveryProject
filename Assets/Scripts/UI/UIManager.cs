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

    [Header("SettingPanel")]
    [SerializeField] GameObject optionPanel;
    public bool onOption = true;

    [Header("GameOverPanel")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject OverImage;
    [SerializeField] GameObject timeOutImage;

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

    int timeCountdown = 4;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject countdownPanel;
    void Start()
    {
        //Events
        GameEvents.gameEvents.onUpdateStatusPlayer += updateStatusPlayer;
        GameEvents.gameEvents.onStartGame += (() => showTimeText = true);
        GameEvents.gameEvents.onFinishGame += OnShowFinishPanel;
        GameEvents.gameEvents.onCountdown += (() => InvokeRepeating("OnCountDown", 0, 1));
        GameEvents.gameEvents.onGameOver += OnGameOverPanel;

        //Setting
        BehavSlider.maxValue = gamePlay.MaxBehavPlayer;
        BehavSlider.value = BehavSlider.maxValue - gamePlay.currentBehavPlayer;
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

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverPanel.activeInHierarchy) 
        {
            OnOption();
        }
    }
    private void updateStatusPlayer() //ลดเมื่อผู้เล่นพฤติพฤติกรรมไม่ดี
    {
        BehavSlider.value = BehavSlider.maxValue - gamePlay.currentBehavPlayer;
        //BehavSlider.value =  gamePlay.currentBehavPlayer;
    }

    void OnOption()
    {
        if (onOption)
        {
            if (!optionPanel.activeInHierarchy)
            {
                onOption = false;
                optionPanel.SetActive(true);
                UIAnimation.uiAnimaInstance.OnOptionPanel();

            }
            else
            {
                optionPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    string displayTimer()
    {
        float timer = gamePlay.LimitTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);
        string showtime = string.Format("{0:00}   {1:00}", minutes, seconds);
        return showtime;
    }
    void OnCountDown()  // invoke ใน Countdown Event
    {
        countdownPanel.SetActive(true);
        timeCountdown--;
        countdownText.text = timeCountdown.ToString();
        if (timeCountdown < 1)
        {
            countdownText.text = "Start";
            StartCoroutine(delayCountdown());
        }
    }
    IEnumerator delayCountdown() 
    {
        yield return new WaitForSeconds(0.5f);
        CancelInvoke("OnCountDown");
        countdownPanel.SetActive(false);
        Debug.Log("Start Game");
        GameEvents.gameEvents.startGame();
    }
    string displaySpeed(float speedPlayer) 
    {
        int speed = Mathf.RoundToInt(speedPlayer);
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
        UIAnimation.uiAnimaInstance.OnfinishPanel();
    }
    void OnGameOverPanel() 
    {
        gameOverPanel.SetActive(true);
        if (gamePlay.LimitTime <= 0)
        {
            timeOutImage.SetActive(true);
            OverImage.SetActive(false);
            UIAnimation.uiAnimaInstance.OnOverPanel(timeOutImage);
        }
        else
        {
            OverImage.SetActive(true);
            UIAnimation.uiAnimaInstance.OnOverPanel(OverImage);
        }
    }
    public void changeImgTurnlight(int i)
    {
        if (i == 1) // left
        {
            Turnlight[1].sprite = ImgTurnlight[2];
            if (LightOn)
            {
                Turnlight[0].sprite = ImgTurnlight[0];
                LightOn = false;
            }
            else
            {
                Turnlight[0].sprite = ImgTurnlight[1];
                LightOn = true;
            }
        }
        else if (i == 2)// right
        {
            Turnlight[0].sprite = ImgTurnlight[0];
            if (LightOn)
            {
                Turnlight[1].sprite = ImgTurnlight[2];
                LightOn = false;
            }
            else
            {
                Turnlight[1].sprite = ImgTurnlight[3];
                LightOn = true;
            }
        }
        else
        {
            LightOn = false;
            Turnlight[0].sprite = ImgTurnlight[0];
            Turnlight[1].sprite = ImgTurnlight[2];
            
        }
    }
}
