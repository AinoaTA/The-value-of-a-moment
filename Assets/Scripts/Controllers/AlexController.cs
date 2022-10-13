using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform exitTransform;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        ATuCasa();
    }

    void Update()
    {
    }

    private void ATuCasa()
    {
        navMeshAgent.SetDestination(exitTransform.position);
    }
}
