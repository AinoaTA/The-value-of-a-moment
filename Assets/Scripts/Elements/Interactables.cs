using UnityEngine;

public class Interactables : MonoBehaviour
{

    [Header("Data")]
    public string nameInteractable;
    private int cameraID;

    [Header("Options")]
    public int totalOptions = 1;
    public bool m_Done;
    public float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl;

    //[Header("Calendar extra")]
    //public float m_ExtraAutoControlCalendar;
    //public TaskType taskAssociated;

    [Header("Others")]
    public GameObject OptionsCanvas;
    public Animator anim;

    private Material[] m_Material;

    public virtual bool GetDone() { return m_Done; }
    public virtual void Interaction(int optionNumber) {  }
    public virtual void ExitInteraction() { }
    [HideInInspector]public bool showing = false;
    protected bool actionEnter;

    private void Update()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.Init)
            HideCanvas();
    }

    private void Start()
    {
        //if (gameObject.GetComponent<Renderer>() != null)
        //{
        //    m_Material = gameObject.GetComponent<Renderer>().materials;
        //}
        cameraID = GameManager.GetManager().cameraController.GetID(nameInteractable);
    }

    public virtual void ShowCanvas()
    {
        //if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && !showing)
        //{
        //    showing = true;
        //    anim.SetBool("Showing", showing);
        //}
    }

    public virtual void HideCanvas()
    {
        //if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && showing)
        //{
        //    showing = false;
        //    anim.SetBool("Showing", showing);
        //}
    }

    public virtual void ResetInteractable()
    {
        m_Done = false;
    }

    private void OnMouseOver()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing && !actionEnter)
        {
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && showing && !actionEnter)
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
        //    if (GameManager.GetManager().calendarController.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.m_DayState, taskAssociated.calendar.type))
        //    {
        //        taskAssociated.Done();
        //        GameManager.GetManager().Autocontrol.AddAutoControl(m_ExtraAutoControlCalendar);
        //    }
        //}
    }
}
