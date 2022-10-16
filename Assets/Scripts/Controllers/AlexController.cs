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

    void Update()
    {
        if (navMeshAgent.enabled)
            prop.localRotation = Quaternion.Euler(Vector3.zero);


        if (isGone) return;

        if (Vector3.Distance(transform.position, exitTransform.position) < .2f)
            PaCasa();

        if (Vector3.Distance(transform.position, exitTransform.position) < .2f)
        {
            gameObject.SetActive(false);
            // TODO: Algun sonido de puerta o algo??
            GameManager.GetManager().dialogueManager.SetDialogue("D2PostCafe", delegate
            {
                GameManager.GetManager().blockController.BlockAll(false);
            });
        }
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (isGone)
                {
                    GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_AlexSeVa");
                }
                GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex", delegate
                {
                    // Permitir que Elle elija
                    GameManager.GetManager().dialogueManager.SetDialogue("D2ConvAlex_");//, delegate
                    //{
                    //    StartCoroutine(Room());
                    //});
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
        if (Vector3.Distance(cam.position, transform.position) > 7f) return;
        if (yaVisto) return;
        yaVisto = true;
        Debug.Log("Me estas mirando o k puta");
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1_MirarAlex", delegate
        {
            InteractableBlocked = false;
        });
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
        yield return new WaitUntil(()=>Vector3.Distance(transform.position, exitTransform.position) < 1f);
        GameManager.GetManager().blockController.BlockAll(false);
        gameObject.SetActive(false);
       
    }

    //IEnumerator Room()
    //{
    //    navMeshAgent.SetDestination(cuartoTransform.position);
    //    // yield return new WaitUntil(() => navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);
    //    mochilaHand.SetActive(true);
    //    mochilaRoom.SetActive(false);
    //    yield return null;
    //    //PaCasa();
    //}
    //IEnumerator DelayRoutine()
    //{

    //    StartCoroutine(Room());
    //    yield return new WaitUntil(() => navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);
    //    navMeshAgent.SetDestination(exitTransform.position);
    //}

    //private IEnumerator MePiroDeCasa()
    //{
    //    yield return new WaitForSecondsRealtime(4);
    //    PaCasa();
    //}
}
