using UnityEngine;

public class BucketController : Interactables, ITask
{
    [SerializeField] private enum TypeBucket { CLOTHES, TRASH }
    [SerializeField] private TypeBucket type = TypeBucket.TRASH;

    [HideInInspector] public int currCapacity;
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private int trashGot;

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
    #region OnMouse
    private void OnMouseEnter()
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
                GameManager.GetManager().interactableManager.LookingAnInteractable(null);
                break;
            default:
                break;
        }

        base.SetCanvasValue(false);
    }

    public void SomethingCleaned()
    {
        GameManager.GetManager().autocontrol.AddAutoControl(m_MinAutoControl);
        currCapacity++;
        if (currCapacity >= maxCapacity)
        {
            GameManager.GetManager().dayNightCycle.TaskDone();
            CheckDoneTask();
        }
    }

    public override void ResetInteractable()
    {
        currCapacity = 0;
        trashGot = 0;
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
