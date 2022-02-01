using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public GameObject mainTestPanel;
    public GameObject section1Panel;
    public GameObject section2Panel;
    public GameObject section3Panel;


    // Start is called before the first frame update
    void Start()
    {
        mainTestPanel.SetActive(true);
        section1Panel.SetActive(false);
        section2Panel.SetActive(false);
        section3Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Section1()
    {
        mainTestPanel.SetActive(false);
        section1Panel.SetActive(true);
    }

    public void Section2()
    {
        mainTestPanel.SetActive(false);
        section2Panel.SetActive(true);
    }

    public void Section3()
    {
        mainTestPanel.SetActive(false);
        section3Panel.SetActive(true);
    }
}
