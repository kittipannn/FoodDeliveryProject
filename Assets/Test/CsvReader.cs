using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CsvReader : MonoBehaviour
{
    public TextAsset DataTextAsset;
    public Text Q;
    public Text Ans1;
    public Text Ans2;
    public Text Ans3;
    public Text Ans4;

    void Start()
    {
        readCSV();
    }

    void readCSV()
    {
        string[] data = DataTextAsset.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSIze = data.Length / 6 - 1;

        for (int i = 1; i < 2; i++)
        {
            Debug.Log(data[6 * (i + 1)]);
            Q.text = data[6 * (i + 1)];
            Ans1.text = data[6 * (i + 1) + 1];
            Ans2.text = data[6 * (i + 1) + 2];
            Ans3.text = data[6 * (i + 1) + 3];
            Ans4.text = data[6 * (i + 1) + 4];
        }

    }
}
