using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardAccel = 3f;
    public float reverseAccel = 2f;
    public float currentSpeed;
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
        rb.transform.parent = null;
        currentSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 100f;
            //currentSpeed += Time.deltaTime *2;
            currentSpeed = rb.velocity.magnitude;
            //if (currentSpeed >= 5)
            //{
            //    speedInput = 4000f;
            //}
            //if (currentSpeed >= 8)
            //{
            //    speedInput = 5000f;
            //}
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 100f;
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            currentSpeed = 0f;
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
