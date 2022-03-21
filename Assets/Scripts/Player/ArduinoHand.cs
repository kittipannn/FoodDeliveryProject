using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoHand : MonoBehaviour
{
    [Header("ArduinoValue")]
    SerialPort stream;
    [SerializeField] string port; //Change port 

    string AllvalueFromArdu;
    string valueFromArduSpeed;
    string valueFromArduSteering;
    string valueFromArduturningLight;
    string valueFromArduBrake;
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
    }

    void Update()
    {
        try
        {
            //Value from Arduino = Steer,Speed,Break,Turn signal

            //--- Speed ---
            AllvalueFromArdu = stream.ReadLine();
            valueFromArduSpeed = AllvalueFromArdu;
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
    }
}
