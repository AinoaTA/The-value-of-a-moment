using System.Collections;
using UnityEngine;

public class Window : Interactables, ITask
{
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject tutorial;
    private GameObject minigameCanvas = null;
    private Vector3 initPos;
    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 3.5f;
    private bool isOpen = false;
    private bool gameInitialized = false;
    private bool tutorialShowed = false;

    [SerializeField] private float distance;

    private static FMOD.Studio.EventInstance streetAmb;

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
        Calendar.CalendarController cal = GameManager.GetManager().calendarController;
        if (cal.CheckReward(taskAssociated))
        {
            if (cal.CheckTimeTaskDone(GameManager.GetManager().dayController.GetTimeDay(), taskAssociated.calendar.type))
            {
                TaskCompleted();
                cal.GetTaskReward(this);
            }
        }
    }

    #endregion

    #region Inherit Interactable methods

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (!isOpen)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    gameInitialized = true;
                    // Inicia minijuego
                    GameManager.GetManager().cameraController.StartInteractCam(4);
                    GameManager.GetManager().canvasController.UnLock();
                }
                break;
            case 2:
                Debug.Log(2);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                break;
        }
    }

    public override void ExitInteraction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom Out", transform.position);
        gameInitialized = false;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void ResetWindow()
    {
        interactDone = false;
        isOpen = interactDone;
        gameInitialized = false;
        glass.transform.position = initPos;
    }

    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        StartCoroutine(HideTutorial());
        Animator animator = tutorial.GetComponent<Animator>();
        if (animator != null) animator.SetBool("show", true);
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

    #endregion

    private void Start()
    {
        streetAmb = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Amb/Street");

        //streetAmb.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SetTask();
        minigameCanvas = tutorial;
        minigameCanvas.SetActive(false);
        minHeight = glass.transform.position.y;
        initPos = glass.transform.position;

    }
    private void Update()
    {
        if (gameInitialized)
        {
            if (!tutorialShowed)
                InitTutorial();
        }
    }

    #region OnMouse Region
    void OnMouseDown()
    {
        if (gameInitialized)
        {
            zWorldCoord = Camera.main.WorldToScreenPoint(glass.transform.position).z;
            // offset = World pos - Mouse World pos
            mOffset = glass.transform.position.y - GetMouseYaxisAsWorldPoint();
        }
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isOpen)
        {
            if (tutorialShowed) tutorial.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Window Scratch", transform.position);
            float height = glass.transform.position.y;
            float displacement = GetMouseYaxisAsWorldPoint() + mOffset;
            

            if (displacement < minHeight)
                height = minHeight;

            else if (displacement < maxHeight)
                height = displacement;

            else if (displacement > maxHeight)
            {
                height = maxHeight;
                interactDone = isOpen = true;
            }
            glass.transform.position = new Vector3(glass.transform.position.x, height, glass.transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (interactDone && gameInitialized)
            WindowDone();
    }
    #endregion
    private void WindowDone()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Window Clank", transform.position);
        streetAmb.start();
        streetAmb.release();
        ExitInteraction();
        CheckDoneTask();
        OptionComplete();
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        GameManager.GetManager().StartThirdPersonCamera();
        minigameCanvas.SetActive(false);
        isOpen = true;
        interactDone = true;
        GameManager.GetManager().dayController.TaskDone();
    }

    private float GetMouseYaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).y;
    }
}
