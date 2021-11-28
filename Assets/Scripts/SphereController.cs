using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SphereController : MonoBehaviour
{
    [Header("ArduinoValue")]
    SerialPort stream;
    [SerializeField] string port; //Change port 
    string AllvalueFromArdu;
    string valueFromArduSpeed;
    string valueFromArduSteering;
    string valueFromArduGas;
    string valueFromArduBrake;


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
    public float reverseAccel = 2f;
    public float turnStrength = 180f;
    //public float gravityForce = 10f;

    private float speedInput;
    private float turnInput;

    //private bool grounded;
    //public LayerMask whatIsground;
    //public float groundRayLength = 0.5f;

    public Transform steerAndWheel;
    public float maxSteerTurn = 25f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM3") { port = mysps; break; }
        }
        stream = new SerialPort(port, 9600);

        if (!stream.IsOpen)
        {
            print("Opening " + port + ", baud 9600");
            stream.Open();
            stream.ReadTimeout = 100;
            stream.Handshake = Handshake.None;
            if (stream.IsOpen) { print("Open"); }
        }
        rb.transform.parent = null;
        playerSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //Value from Arduino = Steer,Speed,Break,Turn signal

            //--- Speed ---
            AllvalueFromArdu = stream.ReadLine();
            valueFromArduSpeed = AllvalueFromArdu;
            playerSpeed = int.Parse(valueFromArduSpeed);
            //valueFromArduSteering = vec3[0];
            //valueFromArduGas = vec3[1];
            //valueFromArduBrake = vec3[2];
            //valueFromArduSteering = stream.ReadLine();
            //valueFromArduGas = stream.ReadLine();
            //valueFromArduBrake = stream.ReadLine();
            //speedInput = float.Parse(valueFromArduGas);
            ////breakInput = float.Parse(valueFromArduBrake);
            //turnInput = float.Parse(valueFromArduSteering);
            //Debug.Log(AllvalueFromArdu);
            //Debug.Log(valueFromArduSteering);
            //Debug.Log(valueFromArduGas);
            //Debug.Log(valueFromArduBrake);
        }
        catch { }

        if (playerSpeed >= PlayerSetting.maxSpeedValue)
            playerSpeed = PlayerSetting.maxSpeedValue;
        else if (playerSpeed <= PlayerSetting.minSpeedValue)
            playerSpeed = 0;

        //if (playerSpeed >= PlayerSetting.minSpeedValue)
        //    move = true;
        //else
        //    move = false;

        inputHand();
    }
    void inputHand()
    {
        speedInput = 0f;

       if (playerSpeed >= PlayerSetting.minSpeedValue)
       {
            speedInput = playerSpeed * 10f;
       }

       
        //else if (Input.GetAxis("Vertical") < 0)
        //{
        //    speedInput = Input.GetAxis("Vertical") * reverseAccel * 100f;
        //}
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
            //currentSpeed += Time.deltaTime *2;
            playerSpeed = rb.velocity.magnitude;
            //if (currentSpeed >= 5)
            //{
            //    speedInput = 4000f;
            //}
            //if (currentSpeed >= 8)
            //{
            //    speedInput = 5000f;
            //}
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

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = rb.transform.position;
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(speedInput) > 0)
        {
            rb.AddForce(transform.forward * speedInput);
        }
    }
}
