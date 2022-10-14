using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : Interactables
{
    public Transform exitTransform;

    private NavMeshAgent navMeshAgent;
    private bool isGone = false, yaVisto = false;
    private Transform camera;

    private void Awake()
    {
        GameManager.GetManager().alexController = this;
    }

    void Start()
    {
        camera = Camera.main.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        InteractableBlocked = true;
    }

    void Update()
    {
        if (isGone) return;

        if (Vector3.Distance(transform.position, exitTransform.position) < .2f)
            gameObject.SetActive(false);
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if(isGone)
                {
                    GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_AlexSeVa");
                }
                GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex");
                break;
        }
    }

    public override void ExitInteraction()
    {
        base.ExitInteraction();
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(camera.position, transform.position) > 7f) return;
        if (yaVisto) return;
        Debug.Log("Me estas mirando o k puta");
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex", delegate
        {
            yaVisto = true;
            InteractableBlocked = false;
        });
        StartCoroutine(MePiroDeCasa());
    }

    public void PaCasa()
    {
        Debug.Log("Me voy");
        navMeshAgent.SetDestination(exitTransform.position);
        isGone = true;
    }

    private IEnumerator MePiroDeCasa()
    {
        yield return new WaitForSecondsRealtime(4);
        PaCasa();
    }
}
