using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    [SerializeField]
    public bool isCorrect = false;
    [SerializeField]
    public QuizManager quizManager;
    public int idButton;
    public void Answer()
    {
        quizManager.NumOfQuestionDone = 1;
        if (isCorrect)
        {
            Debug.Log("Correct!!");
            quizManager.Correct(idButton);
        }
        else
        {
            Debug.Log("Wrong!!");
            quizManager.Wrong(idButton);
        }
    }
}
