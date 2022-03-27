using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public playerSetting PlayerSetting;

    [System.Serializable]
    public class playerSetting
    {
        public int maxSpeedValue;
        public int minSpeedValue;
    }

    [Header("PlayerValue")]
    //Speed
    private float playerSpeed;
    public float PlayerSpeed { get => playerSpeed; }
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
    void Start()
    {
        rb.transform.parent = null;
        playerSpeed = 0;
    }


    void Update()
    {
        valueTurnlight = ArduinoHand.arduino.turnlightArduino;
        Debug.Log(valueTurnlight);
        TurnlightSystem(valueTurnlight);

        //inputKeyboard();
        inputArduino();
    }
    void inputHand()
    {
        speedInput = 0f;

       if (playerSpeed >= PlayerSetting.minSpeedValue)
       {
            speedInput = playerSpeed * 10f;
       }
        else if (Input.GetAxis("Vertical") == 0)
        {
            playerSpeed = 0f;
            speedInput = 0f;
        }

        turnInput = Input.GetAxis("Horizontal");

        steerAndWheel.localRotation = Quaternion.Euler(steerAndWheel.localRotation.eulerAngles.x, steerAndWheel.localRotation.eulerAngles.y, turnInput * maxSteerTurn);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = rb.transform.position;
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
        }
        turnInput = Input.GetAxis("Horizontal");
        steerAndWheel.localRotation = Quaternion.Euler(steerAndWheel.localRotation.eulerAngles.x, steerAndWheel.localRotation.eulerAngles.y, turnInput * maxSteerTurn);
        /*Debug.Log(steerAndWheel.localRotation);*/ // ใช้ y z
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        transform.position = rb.transform.position;
    }
    void inputArduino()
    {
        speedInput = 0f;
        forwardAccel = ArduinoHand.arduino.speedArduino;
        
        if (forwardAccel >= currentForwardAccel)
        {
            currentForwardAccel = forwardAccel;
            Debug.Log(currentForwardAccel);
            speedInput = forwardAccel * 40;
            forward = ArduinoHand.arduino.speedArduino / 100;
        }
        else
        {
            currentForwardAccel -= 5;
            speedInput = currentForwardAccel * reverseAccel;;
            forward = forward - 0.01f;
            if (forward <= 0 )
            {
                forward = 0;
            }
        }
        playerSpeed = rb.velocity.magnitude;
        //else if (Input.GetAxis("Vertical") < 0)
        //{
        //    speedInput = Input.GetAxis("Vertical") * reverseAccel * 100f;
        //}
        //else if (Input.GetAxis("Vertical") == 0)
        //{
        //    playerSpeed = 0f;
        //    speedInput = 0f;
        //}
        turnInput = ArduinoHand.arduino.steerArduino / 10;

        steerAndWheel.localRotation = Quaternion.Euler(steerAndWheel.localRotation.eulerAngles.x, steerAndWheel.localRotation.eulerAngles.y, turnInput * maxSteerTurn);
        /*Debug.Log(steerAndWheel.localRotation);*/ // ใช้ y z
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * forward, 0f));
        transform.position = rb.transform.position;
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(speedInput) > 0)
        {
            rb.AddForce(transform.forward * speedInput);
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
