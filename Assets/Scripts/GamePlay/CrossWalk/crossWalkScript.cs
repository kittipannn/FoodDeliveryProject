using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossWalkScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5;
    [SerializeField] float timestart;
    bool countTime = true;
    bool crosswalk = false;
    float currentTime;
    private void Start()
    {
        this.enabled = false;
    }
    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (countTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timestart)
            {
                crosswalk = true;
                countTime = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (crosswalk)
        {
            rb.velocity = Vector3.right * speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StopCollider"))
        {
            rb.velocity = Vector3.zero;
            this.enabled = false;
        }
    }
}
