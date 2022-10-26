using UnityEngine;

public class Cuenco : Interactables, ITask, IDependencies
{
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
            if (cal.CheckTimeTaskDone(GameManager.GetManager().dayController.GetTimeDay(), taskAssociated.calendar.type))
            {
                TaskCompleted();
                cal.GetTaskReward(this);
            }
        }
    }

    #endregion

    #region DEPENDENCIES
    public Pienso _pienso;
    private bool hasNecessary_;
    public bool hasNecessary { get => _pienso.grabbed; set => hasNecessary_ = value; }

    #endregion

    public MichiController michiController;
    public GameObject comida;
    private bool hasPienso;

    void Start()
    {
        ResetCuenco();
    }

    #region onmouse

    protected override void OnMouseOver()
    {
        hasNecessary = _pienso.grabbed;
        if (!hasNecessary) return;
        base.OnMouseOver();
    }

    protected override void OnMouseEnter()
    {
        if (!hasNecessary) return;
        base.OnMouseOver();
    }

    #endregion


    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/FeedCat", transform.position);
                if (hasPienso)
                {
                    if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
                    {
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccMino_Alimentar");
                        GameManager.GetManager().IncrementInteractableCount();
                    }
                    comida.SetActive(true);
                    hasPienso = false;
                    InteractableBlocked = true;
                    michiController.FeedMichi();
                }
                break;
        }
    }

    public override void ExitInteraction()
    {
        CheckDoneTask();
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void GrabbedPienso()
    {
        hasPienso = true;
        // Activate canvas options
        InteractableBlocked = false;
    }

    public void ResetCuenco()
    {
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
            SetTask();
        InteractableBlocked = true;
        hasPienso = false;
        comida.SetActive(false);
    }
}
