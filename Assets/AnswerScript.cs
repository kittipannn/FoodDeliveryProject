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

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct!!");
            quizManager.Correct();
        }
        else
        {
            Debug.Log("Wrong!!");
            quizManager.Wrong();
        }
    }
}
