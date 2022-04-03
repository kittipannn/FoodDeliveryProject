using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonManagerStartScene : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    UiAnimationMenu uiAnimationMenu;
    [SerializeField] Button backBtn;

    [Header ("MainMenu")]
    [SerializeField] Button startBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button optionBtn;
    [SerializeField] Button closeOptionBtn;

    [Header("CategoryMenu")]
    [SerializeField] Button theoryBtn;
    [SerializeField] Button practicalBtn;

    [Header("TheoryMenu")]
    [SerializeField] Button prepareBtn;
    [SerializeField] Button examBtn;

    [Header("PracticalMenu")]
    [SerializeField] Button manualBtn;
    [SerializeField] Button tutorialBtn;
    [SerializeField] Button cityBtn;
    [SerializeField] Button countrysideBtn;

    [Header("ManualPanel")]
    [SerializeField] Button hardwareTutorialBtn;
    [SerializeField] Button inGameTutorialBtn;
    [SerializeField] Button prepareYourselfBtn;
    [SerializeField] Button signBtn;
    [SerializeField] Button closePopup;
    [SerializeField] GameObject PopupImage;
    [SerializeField] RawImage manualImage;


    [Header("Panel")]
    [SerializeField] List<GameObject> Panel;
    [SerializeField] GameObject optionPanel;

    [Header("Variable")]
    //int previousPanel;
    int numOfMainPanel = 1;
    int numOfTheoryPanel = 2;
    int numOfPracticalPanel = 3;
    int numOfManualPanel = 4;
    string pathImage = "Image/Manual";
    Stack<int> previousPanel = new Stack<int>();
    private void Awake()
    {
        uiAnimationMenu = gameObject.GetComponent<UiAnimationMenu>();
    }
    private void Start()
    {
        backBtn.onClick.AddListener(() => OnBack());

        //MainMenu Button
        startBtn.onClick.AddListener(() => OnChangePanel(numOfMainPanel));
        exitBtn.onClick.AddListener(() => OnExit());
        optionBtn.onClick.AddListener(() => OnOption());
        closeOptionBtn.onClick.AddListener(() => OnOption());
        //CategoryMenu Button
        theoryBtn.onClick.AddListener(() => OnChangePanel(numOfTheoryPanel));
        practicalBtn.onClick.AddListener(() => OnChangePanel(numOfPracticalPanel));
        //TheoryMenu Button
        prepareBtn.onClick.AddListener(() => OnChangeScene("TheoryScene"));
        examBtn.onClick.AddListener(() => OnChangeScene("ExamScene"));
        //PracticalMenu Button
        manualBtn.onClick.AddListener(() => OnChangePanel(numOfManualPanel));
        tutorialBtn.onClick.AddListener(() => OnChangeScene("TutorialScene"));
        cityBtn.onClick.AddListener(() => OnChangeScene("CityScene"));
        countrysideBtn.onClick.AddListener(() => OnChangeScene("CountrysideScene"));
        //ManualMenu Button
        hardwareTutorialBtn.onClick.AddListener(() => OnimageLoader("HandManual"));
        inGameTutorialBtn.onClick.AddListener(() => OnimageLoader("InGameGuide"));
        prepareYourselfBtn.onClick.AddListener(() => OnimageLoader("PrepareYourself"));
        signBtn.onClick.AddListener(() => OnimageLoader("Sign"));
        closePopup.onClick.AddListener(() => OnCloseImage(PopupImage));
    }
    void OnChangePanel(int nextPanel) 
    {
        for (int i = 0; i < Panel.Count; i++)
        {
            if (Panel[i].activeInHierarchy)
            {
                Panel[i].SetActive(false);
                previousPanel.Push(i);
            }
        }
        Panel[nextPanel].SetActive(true);
    }
    void OnBack() 
    {

        foreach (var panels in Panel)
        {
            panels.SetActive(false);
        }
        Panel[previousPanel.Pop()].SetActive(true);
    }
    void OnExit() 
    {
        Application.Quit();
    }
    void OnChangeScene(string nameScene) 
    {
        sceneLoader.LoadingScene(nameScene);
    }
    void OnimageLoader(string nameImage)
    {
        PopupImage.SetActive(true);
        string imagePath = pathImage + "/" + nameImage;
        Texture2D image = Resources.Load(imagePath) as Texture2D;
        manualImage.texture = image;
        uiAnimationMenu.OpenMunaulPopup();
    }
    void OnCloseImage(GameObject popUp) 
    {
        if (popUp.activeInHierarchy == true)
        {
            popUp.SetActive(false);
        }
    }
    void OnOption() 
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
