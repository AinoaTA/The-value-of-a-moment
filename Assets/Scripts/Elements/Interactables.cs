using UnityEngine;
using Calendar;

public class Interactables : MonoBehaviour
{
    [Header("Data")]
    public string nameInteractable;
    private int cameraID;

    [Header("Options")]
    public int totalOptions = 1;
    [SerializeField] protected bool interactDone;
    [SerializeField] protected float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl;
    [SerializeField] protected bool hasDependencies, hasNecessary;

    [Header("Others")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private Animator anim;
    [SerializeField] private Tasks task;

    public virtual bool GetDone() { return interactDone; }

    public virtual void Interaction(int optionNumber)
    {
        actionEnter = true;
        SetCanvasValue(false);
    }

    public virtual void ExitInteraction()
    {
        actionEnter = false;
        SetCanvasValue(false);
        print("exit interaction");
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
    }
    [HideInInspector] public bool showing = false;
    protected bool actionEnter;

    private void Awake()
    {
        task = GetComponent<Tasks>();
    }
    private void Start()
    {
        cameraID = GameManager.GetManager().cameraController.GetID(nameInteractable);
        SetCanvasValue(false);
    }

    public virtual void ResetInteractable()
    {
        interactDone = false;
        actionEnter = false;
        SetCanvasValue(false);
    }

    private void OnMouseEnter()
    {
        if (hasDependencies)
            return;
       
        Show();
    }
    private void OnMouseExit()
    {
        Hide();
    }

    protected void Show()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing && !actionEnter && !interactDone)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    protected void Hide()
    {
        if (showing && !actionEnter && !interactDone)
        {
            showing = false;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        }
    }

    
    public void SetCanvasValue(bool showing_)
    {
        showing = showing_;
        anim.SetBool("Showing", showing_);
    }

    public void CheckDoneTask()
    {
        if (task == null)
            return;

        if (GetDone())
        {
            CalendarController cal = GameManager.GetManager().calendarController;
            if (cal.CheckReward(task.taskAssociated))
            {
                if (cal.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.m_DayState, task.taskAssociated.calendar.type))
                {
                    task.TaskCompleted();
                }
            }
        }
    }
}
