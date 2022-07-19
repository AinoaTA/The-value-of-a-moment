using UnityEngine;

public class Interactables : MonoBehaviour
{

    [Header("Data")]
    public string nameInteractable;
    private int cameraID;

    [Header("Options")]
    public int totalOptions = 1;
    public bool Done;
    public float MaxAutoControl, MiddleAutoControl, MinAutoControl;

    //[Header("Calendar extra")]
    //public float ExtraAutoControlCalendar;
    //public TaskType taskAssociated;

    [Header("Others")]
    public GameObject OptionsCanvas;
    public Animator anim;

    private Material[] Material;

    public virtual bool GetDone() { return Done; }
    public virtual void Interaction(int optionNumber) 
    {
        
        actionEnter = true;
        SetCanvasValue(false);
    }
    public virtual void ExitInteraction() {
        actionEnter = false;
        SetCanvasValue(false);
    }
    [HideInInspector]public bool showing = false;
    protected bool actionEnter;

    private void Update()
    {
        SetCanvasValue(false);
    }

    private void Start()
    {
        cameraID = GameManager.GetManager().cameraController.GetID(nameInteractable);
    }

    public virtual void ResetInteractable()
    {
        Done = false;
        actionEnter = false;
        SetCanvasValue(false);
    }

    private void OnMouseEnter()
    {
        if (GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.GamePlay && !showing && !actionEnter && !Done)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.GamePlay && showing && !actionEnter && !Done)
        {
            showing = false;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        }
    }

    protected void SetCanvasValue(bool showing_)
    {
        anim.SetBool("Showing", showing_);
    }

    public void CheckDoneTask()
    {
        //if (taskAssociated != null && GetDone() && taskAssociated.calendar!=null)
        //{
        //    if (GameManager.GetManager().calendarController.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.DayState, taskAssociated.calendar.type))
        //    {
        //        taskAssociated.Done();
        //        GameManager.GetManager().Autocontrol.AddAutoControl(ExtraAutoControlCalendar);
        //    }
        //}
    }
}
