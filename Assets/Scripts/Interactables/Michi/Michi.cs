using UnityEngine;

[RequireComponent(typeof(MichiController))]
public class Michi : Interactables, ITask
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
    private MichiController controller;

    private void Start()
    {
        SetTask();
        controller = GetComponent<MichiController>();
    }

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        GameManager.GetManager().dialogueManager.SetDialogue("IMino");
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccMino_Acariciar");
                        GameManager.GetManager().IncrementInteractableCount();
                        break;
                    default: break;
                }
                FMODUnity.RuntimeManager.PlayOneShot("event:/NPCs/Cat/Pet", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                controller.PetMichi();

                break;
            case 2:
                break;
        }
    }

    public override void ExitInteraction()
    {
        CheckDoneTask();
        //controller.Walk();
        controller.UnpetMichi();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public override void ResetInteractable()
    {
        TaskReset();
        base.ResetInteractable();
    }
}
