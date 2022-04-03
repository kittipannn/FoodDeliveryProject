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

    AudioSource audioSource;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();
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
        audioSource.Play();
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
            isMoving = false;
            SoundManager.soundInstance.Play("Horn");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            navMeshAgent.speed = 0f;
            audioSource.Pause();
            isMoving = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AiBrakeTrigger"))
        {
            Debug.Log("out");
            navMeshAgent.speed = speedd;
            SoundManager.soundInstance.Pause("Horn");
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
