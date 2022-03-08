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
    GameObject Player;


    [Header("TutorialPanel")]
    private bool showtutorial;
    [SerializeField] GameObject tutorialPanel;

    [Header("FinishPanel")]
    [SerializeField] CheckEvents checkEvents;
    [SerializeField] GameObject finishPanel;
    [SerializeField] List<Image> symbolInEventImg;
    [SerializeField] List<Sprite> symbol;
    [SerializeField] List<TextMeshProUGUI> descriptionText;

    private void Awake()
    {
        showtutorial = PlayerPrefs.GetInt("ShowTutorial") == 1 ? true : false;
    }
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

        // ทำงานเเมื่อ start Scene Tutorial
        OnshowTutorial();
    }

    
    void Update()
    {
        timeText.text = displayTimer();
        speedText.text = displaySpeed(Player.GetComponent<SphereController>().PlayerSpeed);
        //if (showTimeText)
        //    timeText.text = displayTimer();
    }
    private void updateStatusPlayer() //ลดเมื่อผู้เล่นพฤติพฤติกรรมไม่ดี
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

    public void OnshowTutorial() 
    {
        if (!showtutorial)
        {
            showtutorial = true;
            PlayerPrefs.SetInt("ShowTutorial", showtutorial ? 1 : 0);
            tutorialPanel.SetActive(true);
            StartCoroutine(delaysetFalseTutorialPanel());
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }
    IEnumerator delaysetFalseTutorialPanel()
    {
        yield return new WaitForSeconds(5);
        tutorialPanel.SetActive(false);
    }
}
