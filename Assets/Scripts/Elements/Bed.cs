using System.Collections;
using UnityEngine;
public class Bed : Interactables, ITask
{
    public Camera cam;
    public GameObject m_Tutorial;
    private GameObject minigameCanvas = null;
    public GameObject m_SheetBad;
    public GameObject m_Sheet;
    public GameObject interactTextBed;
    public GameObject sleepTextBed;
    private bool gameInitialized;
    Vector3 initPosBadSheet;
    float minDesplacement;
    float maxDesplacement = 2.17f;
    private float zWorldCoord;
    private float mOffset;
    private bool tutorialShowed = false;
    private Vector3 initPosDormirText;
    private Vector3 lastPosDormirText;
    private GameObject badBed;

    #region TASK
    [Space(20)]
    [Header("TASK")]
    [SerializeField] private string nameTask_;
    [SerializeField] private Calendar.TaskType.Task task_;
    [SerializeField] private int extraAutocontrol = 5;
    [SerializeField] private Calendar.TaskType taskType_;
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
            if (cal.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.GetTimeDay(), taskAssociated.calendar.type))
            {
                TaskCompleted();
                cal.GetTaskReward(this);
            }
        }
    }

    #endregion

    private void Awake()
    {
        totalOptions = 2;
        gameInitialized = false;

        initPosDormirText = sleepTextBed.transform.localPosition;
        lastPosDormirText = new Vector3(-0.03f, lastPosDormirText.y, lastPosDormirText.z);
    }
    private void Start()
    {
        SetTask();

        if (m_Tutorial != null)
        {
            minigameCanvas = m_Tutorial;
            minigameCanvas.SetActive(false);
        }
        if (m_SheetBad != null)
        {
            badBed = m_SheetBad.transform.parent.gameObject;
            m_SheetBad.SetActive(true);
            initPosBadSheet = m_SheetBad.transform.position;
            minDesplacement = m_SheetBad.transform.position.x;
        }

    }

    void OnMouseDrag()
    {
        if (gameInitialized && !interactDone)
        {
            if (!tutorialShowed)
                InitTutorial();
            else
                m_Tutorial.SetActive(false);

            float movement = m_SheetBad.transform.position.x;
            float displacement = GetMouseXaxisAsWorldPoint() + mOffset;

            if (displacement < minDesplacement)
            {
                print("Not enough");
                movement = minDesplacement;
            }
            else if (displacement < maxDesplacement)
                movement = displacement;

            else if (displacement > maxDesplacement)
            {
                movement = maxDesplacement;
                interactDone = true;
            }
            m_SheetBad.transform.position = new Vector3(movement, m_SheetBad.transform.position.y, m_SheetBad.transform.position.z);
        }
    }
    private void InitTutorial()
    {
        StartCoroutine(ActivateMinigameCanvas());
        Animator animator = m_Tutorial.GetComponent<Animator>();
        if (animator != null) animator.SetBool("show", true);
        StartCoroutine(HideTutorial());
        tutorialShowed = true;
    }

    private IEnumerator HideTutorial()
    {
        yield return new WaitForSecondsRealtime(8);
        m_Tutorial.SetActive(false);
    }

    private IEnumerator ActivateMinigameCanvas()
    {
        yield return new WaitForSecondsRealtime(1f);
        minigameCanvas.SetActive(true);
    }

    private void OnMouseUp()
    {
        if (interactDone && gameInitialized)
            BedDone();
    }

    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_SheetBad.transform.position).z;
        // offset = World pos - Mouse World pos
        mOffset = m_SheetBad.transform.position.y - GetMouseXaxisAsWorldPoint();
    }

    public void BedDone()
    {

        gameInitialized = false;
        minigameCanvas.SetActive(false);
        interactDone = true;
        cam.cullingMask = -1;
        CheckDoneTask();
        m_Sheet.SetActive(true);
        badBed.SetActive(false);
        interactTextBed.SetActive(false);
        sleepTextBed.transform.localPosition = lastPosDormirText;
        GameManager.GetManager().dayNightCycle.TaskDone();
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
    }

    public void ResetBed()
    {
        GameManager.GetManager().alarm.SetAlarmActive();
        //GameManager.GetManager().Alarm.ResetTime();
        interactDone = false;
        m_Sheet.SetActive(false);
        badBed.SetActive(true);
        sleepTextBed.transform.localPosition = initPosDormirText;
        m_SheetBad.transform.position = initPosBadSheet;
        interactTextBed.SetActive(true);
        gameInitialized = false;
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                if (!interactDone)
                {
                    GameManager.GetManager().cameraController.StartInteractCam(3);
                    gameInitialized = true;
                    GameManager.GetManager().canvasController.UnLock();
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
                    StartCoroutine(ActivateMinigameCanvas());
                }
                break;
            case 2:
                GameManager.GetManager().gameStateController.ChangeGameState(0);
                GameManager.GetManager().canvasController.Lock();

                StartCoroutine(DelayReset());
                break;
            default:
                break;
        }
    }

    private float GetMouseXaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).x;
    }

    private IEnumerator DelayReset()
    {
        //GameManager.GetManager().soundController.QuitAllMusic();
        GameManager.GetManager().canvasController.Pointer.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        GameManager.GetManager().cameraController.StartInteractCam(1);
        GameManager.GetManager().playerController.PlayerSleepPos();
        //GameManager.GetManager().Dialogue.StopDialogue();
        //GameManager.GetManager().Window.ResetWindow();
        GameManager.GetManager().calendarController.GlobalReset();
        GameManager.GetManager().programMinigame.ResetAllGame();
        //GameManager.GetManager().bucket.ResetInteractable();
        //GameManager.GetManager().Mirror.ResetInteractable();
        //GameManager.GetManager().ResetTrash();
        //GameManager.GetManager().Book.ResetInteractable();
        //GameManager.GetManager().VR.ResetVRDay();

        GameManager.GetManager().interactableManager.ResetAll();

        //for (int i = 0; i < GameManager.GetManager().trashes.Count; i++)
        //{
        //    GameManager.GetManager().trashes[i].gameObject.SetActive(true);
        //    GameManager.GetManager().trashes[i].ResetInteractable();
        //}

        //for (int i = 0; i < GameManager.GetManager().Plants.Count; i++)
        //{
        //    GameManager.GetManager().Plants[i].NextDay();
        //    GameManager.GetManager().Plants[i].ResetInteractable();
        //}
        //no borrar hasta que estan tooooooodas las animaciones colocadas aqui.
        Debug.Log("NO FORGET: actions to reset.");
        ResetBed();
        yield return new WaitForSeconds(2);
        GameManager.GetManager().autocontrol.AutocontrolSleep();
        GameManager.GetManager().dayNightCycle.NewDay();
        //GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
    }

    public override void ExitInteraction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom Out", transform.position);
        cam.cullingMask = -1;
        base.ExitInteraction();
        gameInitialized = false;
        cam.cullingMask = -1;
        minigameCanvas.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
    }
}
