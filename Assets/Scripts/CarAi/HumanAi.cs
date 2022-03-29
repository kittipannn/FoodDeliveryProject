using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAi : MonoBehaviour
{
    [SerializeField] GameObject targetPoint1, targetPoint2;
    Collider Trigger1, Trigger2;

    private NavMeshAgent navMeshAgentHuman;

    // Start is called before the first frame update
    void Start()
    {
        Trigger1 = targetPoint1.GetComponent<Collider>();
        Trigger2 = targetPoint2.GetComponent<Collider>();

        navMeshAgentHuman = GetComponent<NavMeshAgent>();

        navMeshAgentHuman.destination = targetPoint1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == Trigger1)
        {
            navMeshAgentHuman.destination = targetPoint2.transform.position;
        }
        else if (other == Trigger2)
        {
            navMeshAgentHuman.destination = targetPoint1.transform.position;
        }
    }


}
