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
    Animator animator;
    private void Start()
    {
        this.enabled = false;
    }
    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
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
            //rb.velocity = Vector3.right * speed;
            animator.SetBool("IsMovingTrigger", true);
            rb.AddRelativeForce(Vector3.right * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stopp"))
        {
            animator.SetBool("IsMovingTrigger", false);
            rb.velocity = Vector3.zero;
            this.enabled = false;
        }
    }
}
