using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonManagerStartScene : MonoBehaviour
{
    [Header ("Button")]
    [SerializeField] Button startBtn;
    [SerializeField] Button tutorialBtn;
    [SerializeField] Button testBtn;
    [SerializeField] Button optionBtn;
    [SerializeField] Button closeOptionBtn;

    [Header("GameObject")]
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject optionPanel;

    private void Start()
    {
        //Setting Button
        startBtn.onClick.AddListener(() => onStartBtn());
        tutorialBtn.onClick.AddListener(() => onTutorialBtn());
        testBtn.onClick.AddListener(() => onTestBtn());
        optionBtn.onClick.AddListener(() => onOptionBtn());
        closeOptionBtn.onClick.AddListener(() => onOptionBtn());
    }
    void onStartBtn()
    {
        sceneLoader.LoadingScene("TutorialScene");
    }
    void onTutorialBtn()
    {
        //sceneLoader.LoadingScene("TutorialScene");
        Debug.Log("Tiutorial");
    }
    void onTestBtn() 
    {
        sceneLoader.LoadingScene("TestScene");
    }
    void onOptionBtn() 
    {
        if (!optionPanel.activeInHierarchy)
        {
            optionPanel.SetActive(true);
        }
        else
        {
            optionPanel.SetActive(false);
        }
    }
}
