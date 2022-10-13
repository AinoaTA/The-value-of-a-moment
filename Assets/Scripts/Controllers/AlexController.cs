using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : Interactables
{
    public Transform exitTransform;

    private NavMeshAgent navMeshAgent;
    private bool isGone = false, yaVisto = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        InteractableBlocked = true;
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

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex", delegate
                {
                    yaVisto = true;
                });
                break;
        }
    }

    public override void ExitInteraction()
    {
        base.ExitInteraction();
    }

    private void OnMouseEnter()
    {
        if (yaVisto) return;
        Debug.Log("Me estas mirando o k puta");
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex", delegate
        {
            yaVisto = true;
            InteractableBlocked = false;
        });
        StartCoroutine(MePiroDeCasa());
    }

    private IEnumerator MePiroDeCasa()
    {
        yield return new WaitForSecondsRealtime(4);
        Debug.Log("Me voy");
        navMeshAgent.SetDestination(exitTransform.position);
    }
}
