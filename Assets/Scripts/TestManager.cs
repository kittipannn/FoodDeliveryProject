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



    // Start is called before the first frame update
    void Start()
    {
        mainTestPanel.SetActive(true);
        section1Panel.SetActive(false);

    }


    public void Section1()
    {
        mainTestPanel.SetActive(false);
        section1Panel.SetActive(true);
    }
}
