using UnityEngine;

public class Interactables : MonoBehaviour
{
    [Header("Interactable Options")]
    public int options = 1;
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
   // public virtual VoiceOff[] GetPhrasesVoiceOff() { return m_HelpPhrasesVoiceOff; }
    public virtual void Interaction(int optionNumber) {  }
    public virtual void ExitInteraction() { }
    public bool showing = false;

    private int cameraID;
    public string nameInteractable;

    private void Update()
    {
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.Init)
            HideCanvas();
    }

    private void Start()
    {
        if (gameObject.GetComponent<Renderer>() != null)
        {
            m_Material = gameObject.GetComponent<Renderer>().materials;
        }

       // cameraID = GameManager.GetManager().cameraController.GetID(nameInteractable);
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
        //if(m_Material != null && m_Material.Length > 0)
        //{
        //    foreach (var material in this.gameObject.GetComponent<Renderer>().materials)
        //    {
        //        // material.color = Color.red;
        //    }
        //}
        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && !showing)
        {
            showing = true;
            anim.SetBool("Showing", showing);
        }

    }

    private void OnMouseExit()
    {
        if (m_Material != null && m_Material.Length > 0)
        {
            for (int i = 0; i < m_Material.Length; i++)
            {
                // TODO: need to recover the m_Material[i].color as such
                // this.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
        }

        if (GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay && showing)
        {
            showing = false;
            anim.SetBool("Showing", showing);
        }
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
