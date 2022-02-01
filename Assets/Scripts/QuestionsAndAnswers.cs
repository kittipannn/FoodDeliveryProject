[System.Serializable]

public class QuestionsAndAnswersList
{
    public QuestionsAndAnswers[] QnA;
}

[System.Serializable]

public class QuestionsAndAnswers
{
    public string Question;
    public string[] Answers = new string[4];
    public int CorrectAnswer;
    public bool AnswerDone;
    public int AnswerFromPlayer;
}
