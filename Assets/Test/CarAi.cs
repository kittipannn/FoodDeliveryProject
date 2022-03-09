using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarAi : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> path = null;
    [SerializeField]
    private float arriveDistance = .3f, lastpointArriveDistance = .1f;
    [SerializeField]
    private float turningAngleOffset = 5;
    [SerializeField]
    private Vector3 currentTargetPosition;

    private int Index = 0;

    private bool stop;

    public bool Stop
    {
        get { return stop; }
        set { stop = value; }
    }

    [field: SerializeField]
    public UnityEvent<Vector2> OnDrive { get; set; } //ใส่ script เคลื่อนที่ของรถ
    void Start()
    {
        if (path == null || path.Count == 0)
        {
            Stop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
