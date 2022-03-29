using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAiNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform FLWheel, FRWheel, RLWheel, RRWheel;

    [SerializeField] private Collider stopTrigger;

    [SerializeField] private float speedd;

    public float wheelSpinningSpeed = 50f;

    private NavMeshAgent navMeshAgent;

    public float stopSecond;

    bool isMoving = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.speed = speedd;
    }
    private void Update()
    {
        if (isMoving == true)
        {
            WheelSpinning();
        }
    }
    public void AiMove()
    {
        navMeshAgent.destination = target.position;
        isMoving = true;
    }

    public void WheelSpinning()
    {
        FLWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        FRWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        RLWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        RRWheel.transform.Rotate(wheelSpinningSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == stopTrigger)
        {
            Debug.Log("Stop");
            isMoving = false;
            StartCoroutine(AiStop());
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("AiBrakeTrigger"))
        {
            Debug.Log("enter");
            navMeshAgent.speed = 0f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AiBrakeTrigger"))
        {
            Debug.Log("out");
            navMeshAgent.speed = speedd;
        }
    }

    IEnumerator AiStop()
    {
        navMeshAgent.speed = 0f;
        yield return new WaitForSeconds(stopSecond);
        navMeshAgent.speed = speedd;
        isMoving = true;
    }
}
