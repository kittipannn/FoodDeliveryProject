using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public QuestionsAndAnswersList QuestionsAndAnswersList = new QuestionsAndAnswersList();

    public GameObject[] options;
    public RawImage QuestionImage;
    public int currentQuestion;
    [SerializeField] protected TextAsset DataTextAsset;
    string imagePath;
    Texture2D image;


    public GameObject StartPanel;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject AnswersPanel;

    public TMP_Text QuestionText;
    public TMP_Text ScoreTxt;

    int totalQuestions = 0;
    public int score;
    private int numOfQuestionDone = 0;
    public int NumOfQuestionDone { set { numOfQuestionDone += value; } }
    [SerializeField] Button answerBtn;
    [SerializeField] Button nextBtn;
    Color ColorBtn;
    private void Awake()
    {
        CSVReader();
    }
    private void Start()
    {
        ColorBtn = options[1].GetComponent<Image>().color;
        answerBtn.onClick.AddListener(() => GenerateQuestionForAnswer());
        answerBtn.onClick.AddListener(() => OnAnswer());
        nextBtn.onClick.AddListener(() => GenerateQuestionForAnswer());
        nextBtn.gameObject.SetActive(false);
        totalQuestions = QuestionsAndAnswersList.QnA.Length;
        GoPanel.SetActive(false);
        GenerateQuestion();
    }
    void CSVReader()
    {
        string[] data = DataTextAsset.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSIze = data.Length / 7 - 1;
        QuestionsAndAnswersList.QnA = new QuestionsAndAnswers[4];// จะใช้ให้เปลลี่ยนเป็น tableSIze

        for (int i = 0; i < 4; i++) // จะใช้ให้เปลลี่ยนเป็น tableSIze
        {
            QuestionsAndAnswersList.QnA[i] = new QuestionsAndAnswers();

            QuestionsAndAnswersList.QnA[i].Question = data[7 * (i + 1)];
            for (int j = 0; j < 4; j++)
            {
                QuestionsAndAnswersList.QnA[i].Answers[j] = data[7 * (i + 1) + (j + 1)];// (j + 1) ตำแหน่งที่ 1- 4 ใน ตาราง csv
            }
            QuestionsAndAnswersList.QnA[i].ImageName = data[7 * (i + 1) + 5];
            QuestionsAndAnswersList.QnA[i].CorrectAnswer = int.Parse(data[7 * (i + 1) + 6]);
            
        }

    }

    public void QuizStart()
    {
        StartPanel.SetActive(false);
    }

    public void BackToQuizMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void ShowAnswerPanel()
    //{
    //    AnswersPanel.SetActive(true);
    //}

    public void CloseAnsPanel()
    {
        AnswersPanel.SetActive(false);
    }

    void QuizOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = "Score: " + score + "/" + totalQuestions;
    }

    public void Correct(int idButton)
    {

        score += 1;
        QuestionsAndAnswersList.QnA[currentQuestion].AnswerFromPlayer = idButton;
        QuestionsAndAnswersList.QnA[currentQuestion].AnswerDone = true;
        GenerateQuestion();
    }

    public void Wrong(int idButton)
    {

        QuestionsAndAnswersList.QnA[currentQuestion].AnswerFromPlayer = idButton;
        QuestionsAndAnswersList.QnA[currentQuestion].AnswerDone = true;
        GenerateQuestion();
    }

    void SetAnswer()
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QuestionsAndAnswersList.QnA[currentQuestion].Answers[i];
            if (QuestionsAndAnswersList.QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    
    void GenerateQuestion()
    {

        if (numOfQuestionDone < QuestionsAndAnswersList.QnA.Length)
        {
            questionDoneDetect();
            QuestionText.text = QuestionsAndAnswersList.QnA[currentQuestion].Question;
            setImgaeQuestion();
            SetAnswer();
        }
        else
        {
            Debug.Log("Out of Questions");
            QuizOver();
        }
    }
    void setImgaeQuestion() 
    {
        if (QuestionsAndAnswersList.QnA[currentQuestion].ImageName != "-")
        {
            Debug.Log(QuestionsAndAnswersList.QnA[currentQuestion].ImageName);

            Debug.Log("LoadImage");
            imageLoader(QuestionsAndAnswersList.QnA[currentQuestion].ImageName);
            setAlphaImage(1);
        }
        else
        {
            Debug.Log("Image");
            QuestionImage.texture = null;
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
        imagePath = "Image/" + DataTextAsset.name + "/" + nameImage;
        image = Resources.Load(imagePath) as Texture2D;
        QuestionImage.texture = image;
        QuestionImage.SetNativeSize();
        QuestionImage.transform.localScale = QuestionImage.transform.localScale / 2;
    }
    List<int> indexQuestion;
    List<int> sequenceQuestion = new List<int>();
    void questionDoneDetect()
    {

        indexQuestion = new List<int>();
        for (int i = 0; i < QuestionsAndAnswersList.QnA.Length; i++)
        {
            if (!QuestionsAndAnswersList.QnA[i].AnswerDone)
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
            options[i].GetComponent<Image>().color = ColorBtn;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QuestionsAndAnswersList.QnA[indexAnswer].Answers[i];
        }
        int indexButton;
        indexButton = QuestionsAndAnswersList.QnA[indexAnswer].CorrectAnswer;
        options[indexButton - 1].GetComponent<Image>().color = Color.green;

        if (QuestionsAndAnswersList.QnA[indexAnswer].CorrectAnswer != QuestionsAndAnswersList.QnA[indexAnswer].AnswerFromPlayer)
        {
            indexButton = QuestionsAndAnswersList.QnA[indexAnswer].AnswerFromPlayer;
            options[indexButton - 1].GetComponent<Image>().color = Color.red;
        }

    }
    
    void GenerateQuestionForAnswer() //ทำงานตอนกดปุ่มเฉลย กับ กดปุ่ม next 
    {
        //if (IndexAnswer < sequenceQuestion.Count)
        //{
        //    nextBtn.gameObject.SetActive(true);
        //    int index = sequenceQuestion[IndexAnswer];
        //    QuestionText.text = QnA[index].Question;
        //    IndexAnswer++;
        //    setButtonAnswer(index);
        //}
        //else
        //{
        //    IndexAnswer = 0;
        //    Debug.Log("Out of Questions");
        //    QuizOver();
        //}
        if (IndexAnswer < sequenceQuestion.Count)
        {
            nextBtn.gameObject.SetActive(true);
            int index = sequenceQuestion[IndexAnswer];
            QuestionText.text = QuestionsAndAnswersList.QnA[index].Question;
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
