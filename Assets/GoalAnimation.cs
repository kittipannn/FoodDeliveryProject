using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * speed) * amplitude) + transform.position.y, transform.position.z);
    }
}
