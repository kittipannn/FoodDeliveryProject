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

    [SerializeField] Button backToMenuBtn;
    [SerializeField] Button doneBtn;
    [SerializeField] Button backToMainBtn;

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
        //SceneManager.LoadScene("Menu");
        PlayerPrefs.DeleteKey("currentData");
        PlayerPrefs.DeleteKey("restartQuiz");
        Application.Quit();
        Debug.Log("Quit");

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
