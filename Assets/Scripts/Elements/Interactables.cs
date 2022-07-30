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
    public bool hasDependencies, hasLeastOne;

    [Header("Others")]
    public GameObject OptionsCanvas;
    public Animator anim;

    private Material[] m_Material;

    public virtual bool GetDone() { return m_Done; }

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

    private void Start()
    {
        //GameManager.GetManager().playerInputs._ExitInteraction += ExitInteraction;
        cameraID = GameManager.GetManager().cameraController.GetID(nameInteractable);
    }

    public virtual void ResetInteractable()
    {
        m_Done = false;
        actionEnter = false;
        SetCanvasValue(false);
    }

    private void OnMouseEnter()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing && !actionEnter && !m_Done)
        {
            if (GetComponent<Plant>())
                if (!GetComponent<Plant>().regadera.grabbed)
                    return;
           
            showing = true;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(this);
        }
    }

    private void OnMouseExit()
    {
        if (showing && !actionEnter && !m_Done)
        {
            showing = false;
            anim.SetBool("Showing", showing);
            GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        }
    }

    public void SetCanvasValue(bool showing_)
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
