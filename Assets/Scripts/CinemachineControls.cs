using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineControls : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] CinemachineBrain cinemachineBrain;


    [SerializeField] GameObject birdEyeCam;
    [SerializeField] GameObject playerCam;
    [SerializeField] float timeSwitchCam;

    private bool startCamCinemachine = false;
    void Start()
    {
        cinemachineStartGame();
        StartCoroutine(delayChangeStartCam());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinemachineBrain.IsBlending && startCamCinemachine)
        {
            ICinemachineCamera birdEye = birdEyeCam.GetComponent<ICinemachineCamera>();
            bool birdEyeLive = CinemachineCore.Instance.IsLive(birdEye);
            if (!birdEyeLive)
            {
                startCamCinemachine = false;
                Debug.Log("Start Game");
                GameEvents.gameEvents.startGame();
            }
        }
    }

    IEnumerator delayChangeStartCam()
    {
        yield return new WaitForSeconds(1);
        startCamCinemachine = true;
    }
    void cinemachineStartGame() 
    {
        Time.timeScale = 1f;
        birdEyeCam.SetActive(true);
        StartCoroutine(delayCinemachineStartGame());
    }
    IEnumerator delayCinemachineStartGame() 
    {
        yield return new WaitForSeconds(timeSwitchCam);
        birdEyeCam.SetActive(false);
    }
}
