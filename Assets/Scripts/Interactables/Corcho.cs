using UnityEngine;

public class Corcho : Interactables, ITask
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

    [SerializeField] CorchoImage[] images;
    [SerializeField] BoxCollider ownCollider;
    int one;
    private void Start()
    {
        SetTask();
    }
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                if (one == 0)
                {
                    one++;
                    GameManager.GetManager().dayController.TaskDone();
                }
                BlockAll(true);
                ownCollider.enabled = false;
                GameManager.GetManager().canvasController.Lock();
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        BlockAll(false);
        ownCollider.enabled = true;
        CheckDoneTask();
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public override void ResetInteractable()
    {
        TaskReset();
        one = 0;
    }

    public void BlockAll(bool t)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].Ready(t);
    }
}
