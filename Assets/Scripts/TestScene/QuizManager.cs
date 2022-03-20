using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class QuizManager : MonoBehaviour
{
    public Button[] categoryStart;
    public QuestionsAndAnswersList questionAndAnswerList;
    public CsvReader data;
    public GameObject[] options;
    public RawImage QuestionImage;
    public int currentQuestion;
    string imagePath;
    int numOfData;
    Texture2D image;

    public GameObject StartPanel;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject AnswersPanel;

    public TMP_Text QuestionText;
    [SerializeField] float posQuestion = 500f;
    [SerializeField] float widthQuestion = 500f;
    public TMP_Text ScoreTxt;

    int totalQuestions = 0;
    public int score;
    private int numOfQuestionDone = 0;
    bool reQuiz;
    public int NumOfQuestionDone { set { numOfQuestionDone += value; } }
    [SerializeField] Button answerBtn;
    [SerializeField] Button nextBtn;
    Color ColorBtn;
    public Sprite defaultImage , trueImgae, falseImage;
    private void Awake()
    {
        reQuiz = PlayerPrefs.GetInt("restartQuiz") == 1 ? true : false;
        Debug.Log(reQuiz);
    }
    private void Start()
    {
        ColorBtn = options[1].GetComponent<Image>().color;
        setBtn();
        //categoryStart.onClick.AddListener(() => OnStartTest());
        nextBtn.gameObject.SetActive(false);
        GoPanel.SetActive(false);
        restartQuiz();
    }
    void setBtn() 
    {
        categoryStart[0].onClick.AddListener(() => OnStartTest(0));
        categoryStart[1].onClick.AddListener(() => OnStartTest(1));
        categoryStart[2].onClick.AddListener(() => OnStartTest(2));
        categoryStart[3].onClick.AddListener(() => OnStartTest(3));
        categoryStart[4].onClick.AddListener(() => OnStartTest(4));
        categoryStart[5].onClick.AddListener(() => OnStartTest(5));
        categoryStart[6].onClick.AddListener(() => OnStartTest(6));
        categoryStart[7].onClick.AddListener(() => OnStartTest(7));
        categoryStart[8].onClick.AddListener(() => OnStartTest(8));
        answerBtn.onClick.AddListener(() => GenerateQuestionForAnswer());
        answerBtn.onClick.AddListener(() => OnAnswer());
        nextBtn.onClick.AddListener(() => GenerateQuestionForAnswer());
    }
    void OnStartTest(int num) 
    {
        switch (num)
        {
            case 0:
                questionAndAnswerList = data.QuestionsAndAnswersList1;
                numOfData = 0;
                break;
            case 1:
                questionAndAnswerList = data.QuestionsAndAnswersList2;
                numOfData = 1;
                break;
            case 2:
                questionAndAnswerList = data.QuestionsAndAnswersList3;
                numOfData = 2;
                break;
            case 3:
                questionAndAnswerList = data.QuestionsAndAnswersList4;
                numOfData = 3;
                break;
            case 4:
                questionAndAnswerList = data.QuestionsAndAnswersList5;
                numOfData = 4;
                break;
            case 5:
                questionAndAnswerList = data.QuestionsAndAnswersList6;
                numOfData = 5;
                break;
            case 6:
                questionAndAnswerList = data.QuestionsAndAnswersList7;
                numOfData = 6;
                break;
            case 7:
                questionAndAnswerList = data.QuestionsAndAnswersList8;
                numOfData = 7;
                break;
            case 8:
                questionAndAnswerList = data.QuestionsAndAnswersList9;
                numOfData = 8;
                break;
        }
        totalQuestions = questionAndAnswerList.QnA.Length;
        PlayerPrefs.SetInt("currentData", numOfData);
        GenerateQuestion();
    }
    void restartQuiz()
    {

        if (reQuiz == true)
        {
            int indexQuestion = PlayerPrefs.GetInt("currentData");
            OnStartTest(indexQuestion);
        }
    }

    public void CloseAnsPanel()
    {
        AnswersPanel.SetActive(false);
    }

    void QuizOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void Correct(int idButton)
    {

        score += 1;
        questionAndAnswerList.QnA[currentQuestion].AnswerFromPlayer = idButton;
        questionAndAnswerList.QnA[currentQuestion].AnswerDone = true;
        GenerateQuestion();
    }

    public void Wrong(int idButton)
    {

        questionAndAnswerList.QnA[currentQuestion].AnswerFromPlayer = idButton;
        questionAndAnswerList.QnA[currentQuestion].AnswerDone = true;
        GenerateQuestion();
    }

    void SetAnswer()
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = questionAndAnswerList.QnA[currentQuestion].Answers[i];
            if (questionAndAnswerList.QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    
    void GenerateQuestion()
    {

        if (numOfQuestionDone < questionAndAnswerList.QnA.Length)
        {
            questionDoneDetect();
            QuestionText.text = questionAndAnswerList.QnA[currentQuestion].Question;
            setImgaeQuestion(currentQuestion);
            SetAnswer();
        }
        else
        {
            Debug.Log("Out of Questions");
            QuizOver();
        }
    }
    void setImgaeQuestion(int indexQuestion) 
    {
        if (questionAndAnswerList.QnA[currentQuestion].ImageName != "-") //Load Image
        {
            imageLoader(questionAndAnswerList.QnA[indexQuestion].ImageName);

            setPosQuestionforImage(true);
            setAlphaImage(1);

        }
        else // No Image
        {
            QuestionImage.texture = null;
            setPosQuestionforImage(false);
            setAlphaImage(0);
        }
    }
    void setAlphaImage(int value) // ทำให้รูปโปร่งใส
    {
        var colorImage = QuestionImage.color;
        colorImage.a = value;
        QuestionImage.color = colorImage;
    }
    void imageLoader(string nameImage)
    {
        QuestionImage.transform.localScale = new Vector3(1, 1, 1);
        imagePath = "Image/" + data.DataTextAsset[numOfData].name + "/" + nameImage;
        image = Resources.Load(imagePath) as Texture2D;
        QuestionImage.texture = image;
        QuestionImage.SetNativeSize();
        QuestionImage.transform.localScale = QuestionImage.transform.localScale / 2;
    }
    void setPosQuestionforImage(bool haveImage) 
    {

        if (haveImage)
        {
            QuestionText.alignment = TextAlignmentOptions.MidlineLeft;
            posQuestion = 365;
            widthQuestion = 1150;
        }
        else
        {
            QuestionText.alignment = TextAlignmentOptions.Center;
            posQuestion = 0;
            widthQuestion = 1580;
        }
        QuestionText.rectTransform.sizeDelta = new Vector2(widthQuestion, 350);
        QuestionText.rectTransform.localPosition = new Vector2(posQuestion, QuestionText.rectTransform.localPosition.y);
    }
    List<int> indexQuestion;
    List<int> sequenceQuestion = new List<int>();
    void questionDoneDetect()
    {

        indexQuestion = new List<int>();
        for (int i = 0; i < questionAndAnswerList.QnA.Length; i++)
        {
            if (!questionAndAnswerList.QnA[i].AnswerDone)
            {
                indexQuestion.Add(i);
            }
        }
        if (indexQuestion.Count > 0)
        {
            currentQuestion = UnityEngine.Random.Range(0, indexQuestion.Count);
            currentQuestion = indexQuestion[currentQuestion];
            sequenceQuestion.Add(currentQuestion);
        }
    }
    int IndexAnswer = 0;
    void setButtonAnswer(int indexAnswer) 
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = false;
            options[i].GetComponent<Image>().sprite = defaultImage;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = questionAndAnswerList.QnA[indexAnswer].Answers[i];
        }
        int indexButton;
        indexButton = questionAndAnswerList.QnA[indexAnswer].CorrectAnswer;
        options[indexButton - 1].GetComponent<Image>().sprite = trueImgae;

        if (questionAndAnswerList.QnA[indexAnswer].CorrectAnswer != questionAndAnswerList.QnA[indexAnswer].AnswerFromPlayer)
        {
            indexButton = questionAndAnswerList.QnA[indexAnswer].AnswerFromPlayer;
            options[indexButton - 1].GetComponent<Image>().sprite = falseImage;
        }

    }
    
    void GenerateQuestionForAnswer() //ทำงานตอนกดปุ่มเฉลย กับ กดปุ่ม next 
    {
        if (IndexAnswer < sequenceQuestion.Count)
        {
            nextBtn.gameObject.SetActive(true);
            int index = sequenceQuestion[IndexAnswer];
            QuestionText.text = questionAndAnswerList.QnA[index].Question;
            setImgaeQuestion(index);
            IndexAnswer++;
            setButtonAnswer(index);
            
        }
        else
        {
            IndexAnswer = 0;
            Debug.Log("Out of Questions");
            QuizOver();
        }
    }
    void OnAnswer() 
    {
        QuizPanel.SetActive(true);
        GoPanel.SetActive(false);
    }
}
