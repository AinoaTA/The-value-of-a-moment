using UnityEngine;
using Calendar;

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
    [SerializeField]private Tasks task;

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

    private void Awake()
    {
        task = GetComponent<Tasks>();
    }
    private void Start()
    {
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
        if (task == null)
            return;

        if (GetDone())
        {
            if (GameManager.GetManager().calendarController.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.m_DayState, task.taskAssociated.calendar.type))
            {
                task.TaskCompleted();
            }
        }
    }
}
