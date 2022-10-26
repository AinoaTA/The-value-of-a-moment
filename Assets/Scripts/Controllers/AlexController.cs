using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlexController : Interactables, ILock
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

        if (timeWithSayNothing < maxTimeToLeave && count && !talking && GameManager.GetManager().counterAlex)
        {
            timeWithSayNothing += Time.deltaTime;

            if (timeWithSayNothing >= maxTimeToLeave && !talking)
                StartCoroutine(CorrectRoutine());
        }
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                InteractableBlocked = true;
                talking = true;
                GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex", delegate
                {
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
        if (yaVisto)
        {
            yaVisto = true;
            GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex");
        }
        if (!GameManager.GetManager().alexVisited) return;
        InteractableBlocked = false;
        base.OnMouseEnter();
    }
    protected override void OnMouseOver()
    {
        if (!GameManager.GetManager().alexVisited) return;
        base.OnMouseOver();
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
