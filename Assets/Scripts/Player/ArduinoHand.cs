using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoHand : MonoBehaviour
{

    public static ArduinoHand arduino;
    public bool Connected = false;
    [Header("ArduinoValue")]
    SerialPort stream;
    [SerializeField] string port; //Change port 

    string AllvalueFromArdu;
    string[] value;
    string valueFromArduBrake; //1
    public int brakeArduino { get { if (Connected) return int.Parse(valueFromArduBrake); else return 0; } }

    string valueFromArduSpeed; //2
    public float speedArduino { get { if (Connected) return float.Parse(valueFromArduSpeed); else return 0; } }
    string valueFromArduSteering; //3
    public float steerArduino { get { if (Connected) return float.Parse(valueFromArduSteering); else return 0; } }
    string valueFromArduturningLight; //4
    public int turnlightArduino { get { if (Connected) return int.Parse(valueFromArduturningLight); else return 0; } }
    private void Awake()
    {
        arduino = this;

        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM3") { port = mysps; break; }
            if (mysps != null) { Connected = true; }
        }

    }
    void Start()
    {
        if (Connected)
        {
            stream = new SerialPort(port, 9600);

            if (!stream.IsOpen)
            {
                print("Opening " + port + ", baud 9600");
                stream.Open();
                stream.ReadTimeout = 100;
                stream.Handshake = Handshake.None;
                if (stream.IsOpen) { print("Open"); }
            }
        }
    }

    void Update()
    {
        if (Connected)
        {
            try
            {
                //Value from Arduino = Steer,Speed,Break,Turn signal

                //--- Speed ---
                AllvalueFromArdu = stream.ReadLine();
                value = AllvalueFromArdu.Split(new char[] { ',' });
                valueFromArduBrake = value[0];
                valueFromArduSpeed = value[1];
                valueFromArduSteering = value[2];
                valueFromArduturningLight = value[3];
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

        }
    }
}
