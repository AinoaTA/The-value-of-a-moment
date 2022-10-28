using UnityEngine;

public class BucketController : Interactables, ITask, IDependencies
{
    [SerializeField] private enum TypeBucket { CLOTHES, TRASH }
    [SerializeField] private TypeBucket type = TypeBucket.TRASH;

    [HideInInspector] public int currCapacity;
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private int trashGot;
    public InventoryTrashUI inventoryUI;

    #region DEPENDENCIES
    private bool hasNecessary_;
    public bool hasNecessary { get => hasNecessary_; set => hasNecessary_ = value; }

    #endregion

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
    #region OnMouse
    protected override void OnMouseEnter()
    {
        switch (type)
        {
            case TypeBucket.CLOTHES:
                trashGot = GameManager.GetManager().trashInventory.dirtyClothesCollected;
                break;
            case TypeBucket.TRASH:
                trashGot = GameManager.GetManager().trashInventory.trashCollected;
                break;
        }
        hasNecessary = trashGot > 0;

        if (!hasNecessary)
            return;

        base.Show();
    }
    protected override void OnMouseOver()
    {
        hasNecessary = trashGot > 0;
        if (!hasNecessary)
            return;
        base.OnMouseOver();
    }

    private void OnMouseExit()
    {
        base.Hide();
    }
    #endregion
    private void Start()
    {
        SetTask();
    }
    public override void Interaction(int optionNumber)
    {
        switch (optionNumber)
        {
            case 1:
                if (type == TypeBucket.CLOTHES)
                {
                    GameManager.GetManager().trashInventory.RemoveDirtyClothes(this);
                }
                else if (type == TypeBucket.TRASH)
                {
                    GameManager.GetManager().trashInventory.RemoveTrash();
                }
                trashGot = 0;
                GameManager.GetManager().dayController.TaskDone();
                GameManager.GetManager().dialogueManager.SetDialogue("IRopaSucia");
                GameManager.GetManager().interactableManager.LookingAnInteractable(null);
                break;
            default:
                break;
        }

        base.SetCanvasValue(false);
    }

    public void SomethingCleaned()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Clothes Drop", transform.position);
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        currCapacity++;
        if (currCapacity >= maxCapacity)
        {
            CheckDoneTask();
        }
    }

    public override void ResetInteractable()
    {
        currCapacity = 0;
        trashGot = 0;
        inventoryUI.ResetInventory();
    }
}
