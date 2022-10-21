using UnityEngine;
public class Limpiar : GeneralActions, ITask
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
    private void Start()
    {
        SetTask();
    }
    public override void EnterAction()
    {
        switch (GameManager.GetManager().dayController.GetTimeDay())
        {
            case DayController.DayTime.Manana:
                break;
            case DayController.DayTime.MedioDia:
                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                break;
            case DayController.DayTime.Tarde:
                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                break;
            case DayController.DayTime.Noche:
                GameManager.GetManager().dialogueManager.SetDialogue("Anochece", canRepeat: true);
                break;
            default:
                break;
        }
        CheckDoneTask();
        InteractableBlocked = true;
        GameManager.GetManager().dayController.TaskDone();
        base.EnterAction();
    }

    public override void ExitAction()
    {
        base.ExitAction();
    }

    public override void ResetObject()
    {
        TaskReset();
        base.ResetObject();
    }
}
