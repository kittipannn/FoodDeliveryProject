using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemBankManager : MonoBehaviour
{
    SceneLoader sceneLoader;
    [Header("Button")]
    [SerializeField] Button nextImageBtn;
    [SerializeField] Button previousImageBtn;
    [SerializeField] Button backToMainBtn;
    [SerializeField] Button backToMainMenuBtn;

    [Header("Section Button")]
    [SerializeField] Button section_1Btn;
    [SerializeField] Button section_2Btn;
    [SerializeField] Button section_3Btn;
    [SerializeField] Button section_4Btn;
    [SerializeField] Button section_5Btn;
    [SerializeField] Button section_6Btn;
    [SerializeField] Button section_7Btn;
    [SerializeField] Button section_8Btn;
    [SerializeField] Button section_9Btn;

    [Header("Objects")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject ShowImagePanel;
    [SerializeField] RawImage Display;
    [SerializeField] RectTransform imageListObject;
    [SerializeField] TMP_Text pageNum;


    int posYtoResetImage = -583;
    string imagePath;
    string imageName = "Section";
    int section = 1;
    int indexImage = 1;
    int numberOfImage = 15;

    private void Awake()
    {
        sceneLoader = this.gameObject.GetComponent<SceneLoader>();
        setButton();
    }
    void Start()
    {
        checkFirstAndLastImage();
    }

    void setButton()
    {
        section_1Btn.onClick.AddListener(() => selectSection(1, 15));
        section_2Btn.onClick.AddListener(() => selectSection(2, 15));
        section_3Btn.onClick.AddListener(() => selectSection(3, 2));
        section_4Btn.onClick.AddListener(() => selectSection(4, 13));
        section_5Btn.onClick.AddListener(() => selectSection(5, 20));
        section_6Btn.onClick.AddListener(() => selectSection(6, 2));
        section_7Btn.onClick.AddListener(() => selectSection(7, 21));
        section_8Btn.onClick.AddListener(() => selectSection(8, 45));
        section_9Btn.onClick.AddListener(() => selectSection(9, 22));

        nextImageBtn.onClick.AddListener(() => OnNextImage());
        previousImageBtn.onClick.AddListener(() => OnPreviousImage());
        backToMainBtn.onClick.AddListener(() => setActivePanel());
        backToMainMenuBtn.onClick.AddListener(() => OnBackToMunu());
    }
    void OnNextImage()
    {
        indexImage++;
        OnTextPage();
        checkFirstAndLastImage();
        OnImageLoader();
    }
    void OnPreviousImage()
    {
        indexImage--;
        OnTextPage();
        checkFirstAndLastImage();
        OnImageLoader();

    }
    void OnTextPage() 
    {
        pageNum.text = indexImage + "/" + numberOfImage;
    }
    void selectSection(int Section , int NumOfImage) 
    {
        section = Section;
        numberOfImage = NumOfImage;
        checkFirstAndLastImage();
        setActivePanel();
        OnTextPage();
        OnImageLoader();
    }
    void setActivePanel() 
    {
        if (mainPanel.activeInHierarchy)
        {
            mainPanel.SetActive(false);
            ShowImagePanel.SetActive(true);
        }
        else
        {
            mainPanel.SetActive(true);
            ShowImagePanel.SetActive(false);
            
        }
        indexImage = 1;
    }
    void checkFirstAndLastImage() 
    {
        if (indexImage == 1) // First page = page 1
            previousImageBtn.interactable = false;
        else
            previousImageBtn.interactable = true;

        if (indexImage == numberOfImage)
            nextImageBtn.interactable = false;
        else
            nextImageBtn.interactable = true;
    }
    void OnImageLoader() 
    {
        imagePath = "Image/ItemBank/Section" + section + "/" + imageName + section + " (" + indexImage +")";
        Texture2D image = Resources.Load(imagePath) as Texture2D;
        Display.texture = image;
        imageListObject.anchoredPosition = new Vector3(0, posYtoResetImage , 0);
    }
    void OnBackToMunu() 
    {
        sceneLoader.LoadingScene("MenuScene");
    }
}
