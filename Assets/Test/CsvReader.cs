using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CsvReader : MonoBehaviour
{
    public TextAsset dataText;
    public Text Q;
    public Text Ans1;
    public Text Ans2;
    public Text Ans3;
    public Text Ans4;

    void Start()
    {
        readCSV();
        Q.rectTransform.position = new Vector2(Q.rectTransform.position.x + 500, Q.rectTransform.position.y);
        Q.alignment = TextAnchor.MiddleLeft;
    }

    void readCSV()
    {
        string[] data = dataText.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSIze = data.Length / 6 - 1;

        for (int i = 0; i < 1; i++)
        {
            Debug.Log(data[6 * (i + 1)]);
            Q.text = data[6 * (i + 1)];
            Ans1.text = data[6 * (i + 1) + 1];
            Ans2.text = data[6 * (i + 1) + 2];
            Ans3.text = data[6 * (i + 1) + 3];
            Ans4.text = data[6 * (i + 1) + 4];
        }
        if (data[6 * (0 + 1) + 1] == "-")
        {
            Debug.Log(data[6 * (0 + 1) + 1]);
        }

    }
    
}
