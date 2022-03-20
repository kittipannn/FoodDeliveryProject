using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CsvReader : MonoBehaviour
{

    public QuestionsAndAnswersList QuestionsAndAnswersList1 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList2 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList3 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList4 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList5 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList6 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList7 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList8 = new QuestionsAndAnswersList();
    public QuestionsAndAnswersList QuestionsAndAnswersList9 = new QuestionsAndAnswersList();

    public TextAsset[] DataTextAsset;
    string imagePath;
    Texture2D image;
    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            CSVReader(i);
        }
    }
    

    void CSVReader(int num)
    {
        QuestionsAndAnswersList QuestionsAndAnswersList;
        QuestionsAndAnswersList = QnAList(num);

        string[] data = DataTextAsset[num].text.Split(new char[] { '\n' });
        int tableSIze = data.Length;
        QuestionsAndAnswersList.QnA = new QuestionsAndAnswers[tableSIze-2];// จะใช้ให้เปลลี่ยนเป็น tableSIze

        for (int i = 1; i < tableSIze - 1; i++) // จะใช้ให้เปลลี่ยนเป็น tableSIze
        {
            string[] row = data[i].Split(new char[] { ',' });
            if (row[1] != "")
            {
                QuestionsAndAnswersList.QnA[i-1] = new QuestionsAndAnswers();

                QuestionsAndAnswersList.QnA[i - 1].Question = row[0];
                for (int j = 0; j < 4; j++)
                {
                    QuestionsAndAnswersList.QnA[i - 1].Answers[j] = row[j+1];// (j + 1) ตำแหน่งที่ 1- 4 ใน ตาราง csv
                }
                QuestionsAndAnswersList.QnA[i - 1].ImageName = row[5];
                QuestionsAndAnswersList.QnA[i - 1].CorrectAnswer = int.Parse(row[6]);

            }
        }

    }
    QuestionsAndAnswersList QnAList(int num) 
    {
        switch (num)
        {
            case 0:
                return QuestionsAndAnswersList1;
                break;
            case 1:
                return QuestionsAndAnswersList2;
                break;
            case 2:
                return QuestionsAndAnswersList3;
                break;
            case 3:
                return QuestionsAndAnswersList4;
                break;
            case 4:
                return QuestionsAndAnswersList5;
                break;
            case 5:
                return QuestionsAndAnswersList6;
                break;
            case 6:
                return QuestionsAndAnswersList7;
                break;
            case 7:
                return QuestionsAndAnswersList8;
                break;
            case 8:
                return QuestionsAndAnswersList9;
                break;

            default:
                return QuestionsAndAnswersList1;
        }
        
    }

}
