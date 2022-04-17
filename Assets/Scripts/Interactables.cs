using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool m_Done;
    public string m_NameObject;
    public string m_ResetName;
    public VoiceOff[] m_HelpPhrases;
    public string[] m_InteractPhrases;
    public float m_MaxAutoControl, m_MiddleAutoControl, m_MinAutoControl; 

    public Canvas OptionsCanvas;
    private Animator anim;

    public virtual string NameAction(){ return m_NameObject; }
    public virtual bool GetDone() { return m_Done; }
    public virtual VoiceOff[] GetPhrases() { return m_HelpPhrases; }
    public virtual void Interaction() { }

    private void Start()
    {
        anim = OptionsCanvas.GetComponent<Animator>();
    }

    public virtual void ShowCanvas()
    {
        if (anim && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay )
        {
            anim.SetTrigger("Show");
        }
    }

    public virtual void HideCanvas()
    {
        if (anim && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay)
        {
            anim.SetTrigger("Hide");
        }
    }

    public virtual void ResetInteractable()
    {
        m_NameObject = m_ResetName;
        m_Done = false;
    }
}
