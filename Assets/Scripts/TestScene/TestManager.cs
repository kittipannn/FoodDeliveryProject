using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public GameObject mainTestPanel;
    public GameObject section1Panel;
    SceneLoader sceneLoader;
    [SerializeField] Button backToMenuBtn;
    [SerializeField] Button doneBtn;
    [SerializeField] Button backToMainBtn;

    private void Awake()
    {
        sceneLoader = this.gameObject.GetComponent<SceneLoader>();
    }
    void Start()
    {
        bool reQuiz = PlayerPrefs.GetInt("restartQuiz") == 1 ? true : false;
        if (reQuiz)
        {
            Section1();
        }
        else
        {
            mainTestPanel.SetActive(true);
            section1Panel.SetActive(false);
        }
        setBtn();
        
    }
    void setBtn() 
    {
        backToMenuBtn.onClick.AddListener(() => OnBackToMenu());
        doneBtn.onClick.AddListener(() => OnRestartQuiz());
        backToMainBtn.onClick.AddListener(() => OnBackToMain());
    }
    void OnBackToMenu() 
    {
        sceneLoader.LoadingScene("MenuScene");
        PlayerPrefs.DeleteKey("currentData");
        PlayerPrefs.DeleteKey("restartQuiz");


    }
    void OnBackToMain() 
    {
        PlayerPrefs.DeleteKey("currentData");
        PlayerPrefs.DeleteKey("restartQuiz");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnRestartQuiz() 
    {
        bool restartQuiz = true;
        PlayerPrefs.SetInt("restartQuiz", restartQuiz ? 1 : 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Section1()
    {
        mainTestPanel.SetActive(false);
        section1Panel.SetActive(true);
    }
}
