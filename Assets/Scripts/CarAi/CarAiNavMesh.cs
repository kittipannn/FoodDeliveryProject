using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAiNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform FLWheel, FRWheel, RLWheel, RRWheel;

    public float wheelSpinningSpeed = 50f;

    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        WheelSpinning();
    }
    public void AiMove()
    {
        navMeshAgent.destination = target.position;
    }

    public void WheelSpinning()
    {
        FLWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        FRWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        RLWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        RRWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    }
}
