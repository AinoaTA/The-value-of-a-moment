using System.Collections;
using UnityEngine;

public class Nevera : GeneralActions, ITask
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
        base.EnterAction();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/FridgeOpen", transform.position);
        //GameManager.GetManager().gameStateController.ChangeGameState(3);
        //GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        GameManager.GetManager().dialogueManager.SetDialogue("IPicarAlgo",
        delegate
        {
            StartCoroutine(Delay());
        }, canRepeat: true);
    }

    IEnumerator Delay()
    {
        CheckDoneTask();
        InteractableBlocked = true;
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().dialogueManager.SetDialogue("Ducha", delegate
        {
            GameManager.GetManager().blockController.Unlock("Ducha");
        });
    }

    public override void ResetObject()
    {
        TaskReset();
        base.ResetObject();
    }
    public override void ExitAction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/FridgeClosed", transform.position);
        //if (GameManager.GetManager().interactableManager.currInteractable != null)
        //    GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        //GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        ////canvas.SetBool("Showing", false);
        //GameManager.GetManager().StartThirdPersonCamera();
        //base.ExitAction();
    }
}
