using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float maxSpeed = 50f;
    public float turnStrength = 180f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.transform.position;
        rb.AddForce(transform.forward * forwardAccel);
    }

    private void FixedUpdate()
    {
        //rb.AddForce(transform.forward * forwardAccel);
    }
}
