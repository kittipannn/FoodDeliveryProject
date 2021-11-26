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

    private void Start()
    {
        startBtn.onClick.AddListener(() => onStartBtn());

        testBtn.onClick.AddListener(() => onTestBtn());
    }
    void onStartBtn()
    {
        SceneManager.LoadSceneAsync("TutorialScene");
    }
    void onTutorialBtn() 
    {
        //SceneManager.LoadSceneAsync("TutorialScene");
    }
    void onTestBtn() 
    {
        SceneManager.LoadSceneAsync("TestScene");
    }
}
