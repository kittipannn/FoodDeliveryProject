using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonManagerStartScene : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button tutorialBtn;
    [SerializeField] Button testBtn;
    [SerializeField] SceneLoader sceneLoader;
    

    private void Start()
    {
        startBtn.onClick.AddListener(() => onStartBtn());
        tutorialBtn.onClick.AddListener(() => onTutorialBtn());
        testBtn.onClick.AddListener(() => onTestBtn());
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
}
