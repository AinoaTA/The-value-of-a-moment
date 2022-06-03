using UnityEngine;

public class Interactables : MonoBehaviour
{
    [Header("Interactable Options")]
    public int options = 1;
    public bool m_Done;
    public VoiceOff[] m_PhrasesVoiceOff;
    public VoiceOff[] m_HelpPhrasesVoiceOff;
    public string[] m_AnswersToVoiceOff;
    public string[] m_InteractPhrases;
    public float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl;

    [Header("Calendar extra")]
    public bool m_DoneByCalendar;
    public float m_ExtraAutoControlCalendar;
    public TaskType taskAssociated;

    [Header("Others")]
    public GameObject OptionsCanvas;
    public Animator anim;

    private Material[] m_Material;

    public virtual bool GetDone() { return m_Done; }
    public virtual VoiceOff[] GetPhrasesVoiceOff() { return m_HelpPhrasesVoiceOff; }
    public virtual void Interaction(int optionNumber) {}
    public virtual void ExitInteraction() { }
    public bool showing = false;

    private void Start()
    {
        if (this.gameObject.GetComponent<Renderer>() != null)
        {
            m_Material = this.gameObject.GetComponent<Renderer>().materials;
        }
    }

    public virtual void ShowCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && !showing)
        {
            showing = true;
            anim.SetBool("Showing", showing);
          //  anim.SetTrigger("Show");
        }
    }

    public virtual void HideCanvas()
    {
        if (GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay && showing)
        {
            showing = false;
            anim.SetBool("Showing", showing);
            //  anim.SetTrigger("Hide");
        }
    }

    public virtual void ResetInteractable()
    {
        m_Done = false;
    }

    private void OnMouseOver()
    {
        Debug.Log($"Mouse is over {this.gameObject}");

        if(m_Material != null && m_Material.Length > 0)
        {
            foreach (var material in this.gameObject.GetComponent<Renderer>().materials)
            {
                material.color = Color.red;
            }
        }
    }

    private void OnMouseExit()
    {
        if (m_Material != null && m_Material.Length > 0)
        {
            for (int i = 0; i < m_Material.Length; i++)
            {
                // TODO: need to recover the m_Material[i].color as such
                this.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
        }
    }

    public void CheckDoneTask()
    {
        if (GameManager.GetManager().calendarController.CheckTimeTaskDone(GameManager.GetManager().dayNightCycle.m_DayState, taskAssociated.calendar.type))
        {
            m_DoneByCalendar = true;
            taskAssociated.Done();
            GameManager.GetManager().Autocontrol.AddAutoControl(m_ExtraAutoControlCalendar);
        }
    }
}
