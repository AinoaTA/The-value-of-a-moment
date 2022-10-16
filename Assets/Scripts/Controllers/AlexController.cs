using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : Interactables
{
    public Transform exitTransform, cuartoTransform;
    public Transform rightHand;
    private NavMeshAgent navMeshAgent;
    private bool isGone = false, yaVisto = false;
    private Transform cam;
    public Animator animAlex;
    public Transform prop;
    [SerializeField] float maxTimeToLeave = 10;
    public GameObject mochilaRoom, mochilaHand;
    private void Awake()
    {
        GameManager.GetManager().alexController = this;
    }

    void Start()
    {
        cam = Camera.main.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        InteractableBlocked = true;
        navMeshAgent.enabled = false;
    }

    float timeWithSayNothing;
    bool count;
    bool talking;
    void Update()
    {
        if (navMeshAgent.enabled)
            prop.localRotation = Quaternion.Euler(Vector3.zero);

        if (isGone) return;
        if (Vector3.Distance(transform.position, GameManager.GetManager().playerController.transform.position) < 3f)
            count = true;
        print(timeWithSayNothing);
        if (timeWithSayNothing < maxTimeToLeave && count && !talking)
        {
            timeWithSayNothing += Time.deltaTime;

            if (timeWithSayNothing >= maxTimeToLeave)
                StartCoroutine(CorrectRoutine());
        }
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                //if (isGone)
                //{
                //    GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_AlexSeVa");
                //}
                GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex", delegate
                {
                    talking = true;
                    // Permitir que Elle elija
                    GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex_Op1" , delegate 
                    {
                        GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex_", delegate
                        {
                            StartCoroutine(CorrectRoutine());
                        });
                    });
                });
                break;
        }
    }

    public override void ExitInteraction()
    {
        base.ExitInteraction();
    }

    protected override void OnMouseEnter()
    {
        if (!GameManager.GetManager().alexVisited) return;

        base.OnMouseEnter();

        if (yaVisto) return;
        yaVisto = true;
        Debug.Log("Me estas mirando o k puta");
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex");
        StartCoroutine(CorrectRoutine());
    }

    public void PaCasa()
    {
        isGone = true;
        // StartCoroutine(DelayRoutine());
    }

    IEnumerator CorrectRoutine()
    {
        isGone = true;
        InteractableBlocked = true;
        yield return new WaitForSecondsRealtime(4);
        Debug.Log("Me voy");
        animAlex.Play("Leave");
        yield return new WaitForSeconds(1f);
        animAlex.enabled = false;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(cuartoTransform.position);
        yield return null;
        print(navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);
        print(!navMeshAgent.hasPath);
        yield return new WaitUntil(() => navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && !navMeshAgent.hasPath);
        mochilaHand.SetActive(true);
        mochilaRoom.SetActive(false);
        mochilaHand.transform.SetParent(rightHand.transform);
        mochilaHand.transform.localPosition = Vector3.zero;
        navMeshAgent.SetDestination(exitTransform.position);
        PaCasa();
        yield return new WaitUntil(() => Vector3.Distance(transform.position, exitTransform.position) < 1f);
        GameManager.GetManager().blockController.BlockAll(false);
        GameManager.GetManager().playerController.AudioDialogue();
        yield return null;
        gameObject.SetActive(false);

    }
}
