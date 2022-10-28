using UnityEngine;
public class Cafetera : Interactables, ITask
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
    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Coffee Brew", transform.position);
                //GameManager.GetManager().gameStateController.ChangeGameState(2);
               // GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        GameManager.GetManager().dialogueManager.SetDialogue("ITomarCafe",canRepeat:true);
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("ITomarCafe", canRepeat: true);
                        GameManager.GetManager().IncrementInteractableCount();
                        break;
                    case DayController.Day.three:
                        break;
                    case DayController.Day.fourth:
                        break;
                    default:
                        break;
                }
                break;
        }

        ExitInteraction();
    }

    public override void ExitInteraction()
    {
        //GameManager.GetManager().StartThirdPersonCamera();
        interactDone = true;
        CheckDoneTask();
        base.ExitInteraction();
    }

    public override void ResetInteractable()
    {
        TaskReset();
        base.ResetInteractable();
    }
}
