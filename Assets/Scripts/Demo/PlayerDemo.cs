using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float speedPlayer;
    bool brake;
    float limitSpeed = 15;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        moveMent(speedPlayer);
    }
    void moveMent(float speed) 
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        brake = Input.GetKey(KeyCode.Space);
        if (rigidbody.velocity.magnitude < limitSpeed)
        {
            Vector3 movement = new Vector3(hori, 0, vert).normalized;
            if (movement.magnitude >= 0.1f)
            {
                rotatePlayer(movement);
                movement = movement * speed;
                rigidbody.AddForce(movement, ForceMode.Acceleration);
            }
        }
        if (brake)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    void rotatePlayer(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
