using UnityEngine;

public class Mirror : Interactables
{
    private int m_Counter = 0;
    public string[] m_MirrorInteractPhrases;

    private void Start()
    {
        GameManager.GetManager().Mirror = this;
        m_InteractPhrases = m_MirrorInteractPhrases;
    }

    public override void Interaction(int options)
    {

        switch (options)
        {
            case 1:
                if (!m_Done)
                {
                    if(m_MirrorInteractPhrases.Length > 0)
                    {
                        if (m_Counter >= m_InteractPhrases.Length)
                            m_Counter = 0;

                        GameManager.GetManager().Dialogue.SetDialogue(m_InteractPhrases[m_Counter]);
                        GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
                        m_Counter++;

                        m_Done = true;
                    }
                }
                break;
        }
    }

    public override void ResetInteractable()
    {
        m_Done = false;
    }
}
