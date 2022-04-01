using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimitEvent : MonoBehaviour
{
    [SerializeField] SphereController player;
    string sceneName;
    [SerializeField] float speedCity = 80f;
    [SerializeField] float speedCountrySide = 90f;
    float speedlimitInscene;
    public int NumOfEvent;
    bool checkEvent = false;
    private void Awake()
    {
        if (sceneName == null)
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<SphereController>();
    }
    void Start()
    {
        LimitSpeedInScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.PlayerSpeed >= speedlimitInscene && !checkEvent)
        {
            Debug.Log("Out off limits");
            checkEvent = true;
            GameEvents.gameEvents.checkEvents(NumOfEvent);
            GameEvents.gameEvents.decreaseBehavPlayer(1);
        }
    }
    void LimitSpeedInScene(string name) 
    {
        switch (name)
        {
            case "CityScene":
                speedlimitInscene = speedCity;
                break;
            case "CountrysideScene":
                speedlimitInscene = speedCountrySide;
                break;
            default:
                speedlimitInscene = 150;
                break;

        }
    }
}
