using System.Collections;
using UnityEngine;

public class Plant : Interactables, ITask
{
    public GameObject tutorial;
    private GameObject minigameCanvas = null;

    [SerializeField] private float distance;
    [SerializeField] private WaterCan waterCan; //objecto que movemos interactuando
    [SerializeField] private Regadera regadera; //Objeto que cogemos del suelo
    Vector3 wateringInitialPos;

    [SerializeField] private float timer;
    [SerializeField] private float maxTimer = 3f;
    [SerializeField] private GameObject[] plantProcess;
    private int currPlantPhase = 0;
    private bool gameInitialized;

    private bool tutorialShowed = false;

    #region TASK
    [Space(20)]
    [Header("TASK")]
    [SerializeField] private string nameTask_;
    [SerializeField] private Calendar.TaskType.Task task_;
    [SerializeField] private int extraAutocontrol = 5;
    private Calendar.TaskType taskType_;
    private bool taskCompleted_;

    public int extraAutocontrolByCalendar { get => extraAutocontrol; }
    public bool taskCompleted { get => taskCompleted_; set => taskCompleted_ = value; }
    public string nameTask { get => nameTask_; }
    public Calendar.TaskType.Task task { get => task_; }
    public Calendar.TaskType taskAssociated { get => taskType_; set => taskType_ = value; }
    #endregion

    IEnumerator routine;
    private void Start()
    {
        SetTask();
        minigameCanvas = tutorial;//.transform.parent.gameObject;
        minigameCanvas.SetActive(false);

        if (waterCan != null) waterCan.gameObject.SetActive(false);
        plantProcess[currPlantPhase].SetActive(true);
    }
    #region OnMouse
    private void OnMouseEnter()
    {
        hasNecessary = regadera.grabbed;

        if (!hasNecessary)
            return;

        base.Show();
    }
    private void OnMouseExit()
    {
        base.Hide();
    }
    #endregion

    public override void Interaction(int options)
    {
        if (!hasNecessary)
            return;

        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (!interactDone)
                {
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    gameInitialized = true;
                    timer = 0;

                    GameManager.GetManager().cameraController.StartInteractCam(6);
                    GameManager.GetManager().canvasController.UnLock();

                    StartCoroutine(routine = ActivateWaterCan());
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!tutorialShowed && gameInitialized)
            InitTutorial();

        if (tutorialShowed && waterCan.dragg) tutorial.SetActive(false);
    }


    public override void ExitInteraction()
    {
        if (!gameInitialized)
            return;
        if (routine != null)
            StopCoroutine(routine);

        GameManager.GetManager().StartThirdPersonCamera();
        gameInitialized = false;
        waterCan.gameObject.SetActive(false);
        waterCan.ResetWaterCan();
        actionEnter = false;

        base.ExitInteraction();
    }
    private void FinishInteraction()
    {
        minigameCanvas.SetActive(false);
        waterCan.GrowUpParticle.Play();
        waterCan.gameObject.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        interactDone = true;
        gameInitialized = false;
        waterCan.dragg = false;
        CheckDoneTask();
        GameManager.GetManager().dayNightCycle.TaskDone();
        waterCan.gameObject.SetActive(false);
        actionEnter = false;
    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
        regadera.ResetObject();
        waterCan.ResetWaterCan();
        timer = 0;
        gameInitialized = false;
    }

    public void NextDay()
    {
        //grow
        if (interactDone)
        {
            waterCan.GrowUpParticle.Stop();
            if (currPlantPhase < plantProcess.Length)
            {
                plantProcess[currPlantPhase].SetActive(false);
                currPlantPhase++;
                waterCan.GrowUpParticle.Play();
                plantProcess[currPlantPhase].SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!waterCan.dragg)
            return;

        if (timer <= maxTimer)
            timer += Time.deltaTime;
        else
        {
            FinishInteraction();
        }
    }
    private IEnumerator ActivateWaterCan()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        waterCan.gameObject.SetActive(true);
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        if (tutorial.GetComponent<Animator>() != null)
            tutorial.GetComponent<Animator>().SetBool("show", true);
        tutorialShowed = true;
    }

    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        minigameCanvas.SetActive(true);
    }

    #region TASK
    public void TaskReset()
    {
        taskCompleted = false;
    }

    public void TaskCompleted()
    {
        taskCompleted = true;
    }

    public void RewardedTask()
    {
        Debug.Log("Rewarded Task");
        GameManager.GetManager().autocontrol.AddAutoControl(extraAutocontrolByCalendar);
    }

    public void SetTask()
    {
        GameManager.GetManager().calendarController.CreateTasksInCalendar(this);
    }

    public void CheckDoneTask()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}

