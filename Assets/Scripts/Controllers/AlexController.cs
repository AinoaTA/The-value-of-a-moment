using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform exitTransform;

    private bool isGone = false, meVes = false, yaVisto = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
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

    private void OnMouseEnter()
    {
        Debug.Log("Me estas mirando o k puta");
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex", delegate
        {
            yaVisto = true;
        });
        meVes = true;
        StartCoroutine(MePiroDeCasa());
    }

    private IEnumerator MePiroDeCasa()
    {
        yield return new WaitForSecondsRealtime(4);
        Debug.Log("Me voy");
        navMeshAgent.SetDestination(exitTransform.position);
    }
}
