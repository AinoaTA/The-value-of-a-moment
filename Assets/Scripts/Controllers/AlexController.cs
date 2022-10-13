using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform exitTransform;

    private bool isGone;

    void Start()
    {
        isGone = false;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        ATuCasa();
    }

    void Update()
    {
        if (isGone) return;

        if (Vector3.Distance(transform.position, exitTransform.position) < .2f)
        {
            this.gameObject.SetActive(false);
            isGone = true;
        }
    }

    private void ATuCasa()
    {
        navMeshAgent.SetDestination(exitTransform.position);
    }
}
