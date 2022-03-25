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
        if (player.PlayerSpeed >= speedlimitInscene)
        {
            Debug.Log("Out off limits");
        }
    }
    void LimitSpeedInScene(string name) 
    {
        switch (name)
        {
            case "CityScene":
                speedlimitInscene = speedCity;
                break;
            case "CountrySideScene":
                speedlimitInscene = speedCountrySide;
                break;
            default:
                speedlimitInscene = 150;
                break;

        }
    }
}
