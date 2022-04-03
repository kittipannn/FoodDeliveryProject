using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public playerSetting PlayerSetting;
    bool connectedArduino;
    [System.Serializable]
    public class playerSetting
    {
        public int maxSpeedValue;
        public int minSpeedValue;
    }

    [Header("PlayerValue")]
    //Speed
    private float playerSpeed;
    public float PlayerSpeed { get => playerSpeed * 10; }
    private bool move;
    //Break
    private bool playerBreak;
    public Rigidbody rb;
    public float forwardAccel = 3f;
    private float currentForwardAccel =0;
    public float reverseAccel = 2f;
    public float turnStrength = 180f;
    public float forward;
    //public float gravityForce = 10f;
    //Steer
    private float speedInput;
    public float turnInput;
    public Transform steerAndWheel;
    public float maxSteerTurn = 25f;
    [Header("TurnLight")]
    [SerializeField] UIManager uIManager;
    float timeTurnLight;
    float timetoChange = 0.5f;
    int valueTurnlight = 0;
    //check Event Turnlight
    float turnLightcountTime;
    [SerializeField] float timeToCheck = 2;
    bool checkEvent = false;
    public int NumOfEvent;

    AudioSource audioSource;
    public float minPitch = 1f;
    public float maxPitch = 2f;
    float pitchFromDriving;
    void Start()
    {
        rb.transform.parent = null;
        playerSpeed = 0;
        connectedArduino = ArduinoHand.arduino.Connected;

        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
        pitchFromDriving = 0;
    }


    void Update()
    {
        TurnlightSystem(valueTurnlight);
        inputSystem();
        checkTurnlight();
        if (pitchFromDriving >= maxPitch)
        {
            pitchFromDriving = maxPitch;
        }
        else if (pitchFromDriving <= minPitch)
        {
            pitchFromDriving = minPitch;
        }

        audioSource.pitch = pitchFromDriving;
    }
    void inputSystem() 
    {
        switch (connectedArduino)
        {
            case true:
                inputArduino();
                inputKeyboard();
                break;
            case false:
                inputKeyboard();
                break;
        }
    }
    void inputKeyboard() 
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 100f;
            playerSpeed = rb.velocity.magnitude;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 100f;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            playerSpeed = 0f;
            speedInput = 0f;
            pitchFromDriving -= 0.02f;
        }
        turnInput = Input.GetAxis("Horizontal");
        steerAndWheel.localRotation = Quaternion.Euler(steerAndWheel.localRotation.eulerAngles.x, steerAndWheel.localRotation.eulerAngles.y, turnInput * maxSteerTurn);
   
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        transform.position = rb.transform.position;
    }
    void inputArduino()
    {
        speedInput = 0f;
        forwardAccel = ArduinoHand.arduino.speedArduino;
        valueTurnlight = ArduinoHand.arduino.turnlightArduino;
        int test = 0;
        playerBreak = test == 0 ? true : false ;
        if (forwardAccel >= currentForwardAccel)
        {
            currentForwardAccel = forwardAccel;
            speedInput = forwardAccel * 40;
            forward = ArduinoHand.arduino.speedArduino / 100;
        }
        else
        {
            brake();
            pitchFromDriving -= 0.02f;
        }
        playerSpeed = rb.velocity.magnitude;
        turnInput = ArduinoHand.arduino.steerArduino / 10;

        steerAndWheel.localRotation = Quaternion.Euler(steerAndWheel.localRotation.eulerAngles.x, steerAndWheel.localRotation.eulerAngles.y, turnInput * maxSteerTurn);
        /*Debug.Log(steerAndWheel.localRotation);*/ // ใช้ y z
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * forward, 0f));
        transform.position = rb.transform.position;
    }
    void brake() 
    {
        if (playerBreak)
        {
            currentForwardAccel -= 10 * Time.deltaTime;
            speedInput = currentForwardAccel * reverseAccel; ;
            if (forward > 0)
                forward = forward - 1 * Time.deltaTime;
        }
        else
        {
            currentForwardAccel -= 5 * Time.deltaTime;
            speedInput = currentForwardAccel * reverseAccel; ;
            if (forward > 0)
                forward = forward - 1 * Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(speedInput) > 0)
        {
            rb.AddForce(transform.forward * speedInput);
            pitchFromDriving += 0.01f;
        }
    }
    void checkTurnlight() 
    {
        float angle = steerAndWheel.localEulerAngles.z;
        if (Mathf.Abs(speedInput) > 0 && !checkEvent) // left 70 right 290 from max steer turn
        {
            if (angle == 70 && valueTurnlight != 1)
            {
                if (turnLightcountTime > timeToCheck) 
                { 
                    GameEvents.gameEvents.decreaseBehavPlayer(1);
                    checkEvent = true;
                    GameEvents.gameEvents.checkEvents(NumOfEvent);
                    turnLightcountTime = 0; 
                }
                else turnLightcountTime += Time.deltaTime; ;
            }
            else if (angle == 290 && valueTurnlight != 2)
            {
                if (turnLightcountTime > timeToCheck) 
                { 
                    GameEvents.gameEvents.decreaseBehavPlayer(1);
                    checkEvent = true;
                    GameEvents.gameEvents.checkEvents(NumOfEvent);
                    turnLightcountTime = 0; 
                }
                else turnLightcountTime += Time.deltaTime; ;
            }
            else
            {
                turnLightcountTime = 0;

            }
        }
    }
    public float playerMarkerRot()
    {
        float markerRot;
        markerRot = turnInput;
        return markerRot;
    }
    //TurningLight
    void TurnlightSystem(int value)
    {
        switch (value)
        {
            case 0:
                OnTurnlight(0);
                break;
            case 1:
                OnTurnlight(1); // left
                break;
            case 2:
                OnTurnlight(2); // right
                break;
        }
    }
    void OnTurnlight(int side)
    {
        if (timeTurnLight > timetoChange)
        {
            uIManager.changeImgTurnlight(side);
            timeTurnLight = 0;
        }
        else
        {
            timeTurnLight += Time.deltaTime;
        }
    }

}
