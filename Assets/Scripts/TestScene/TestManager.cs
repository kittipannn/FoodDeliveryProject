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
        mainTestPanel.SetActive(true);
        section1Panel.SetActive(false);
        setBtn();

    }
    void setBtn() 
    {
        backToMenuBtn.onClick.AddListener(() => OnBackToMenu());
        doneBtn.onClick.AddListener(() => OnDone());
        backToMainBtn.onClick.AddListener(() => OnBackToMain());
    }
    void OnBackToMenu() 
    {
        //SceneManager.LoadScene("Menu");
        Application.Quit();
        Debug.Log("Quit");

    }
    void OnBackToMain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnDone() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Section1()
    {
        mainTestPanel.SetActive(false);
        section1Panel.SetActive(true);
    }
}
