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

    public virtual string NameAction(){ return m_NameObject; }
    public virtual bool GetDone() { return m_Done; }
    public virtual VoiceOff[] GetPhrases() { return m_HelpPhrases; }
    public virtual void Interaction() { }

    public virtual void ShowCanvas()
    {
        if (OptionsCanvas.GetComponent<Animator>() && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay )
        {
            OptionsCanvas.GetComponent<Animator>().SetTrigger("Canvas");
        }
    }

    public virtual void ResetInteractable()
    {
        m_NameObject = m_ResetName;
        m_Done = false;
    }
}
