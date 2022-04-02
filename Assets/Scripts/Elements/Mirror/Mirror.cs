using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Interactables
{
    private int m_Counter = 0;
    public string[] m_MirrorInteractPhrases;
    
    private void Awake()
    {
        GameManager.GetManager().Mirror = this;
    }

    public override void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_MirrorInteractPhrases.Length)
                m_Counter = 0;

            GameManager.GetManager().Dialogue.SetDialogue(m_MirrorInteractPhrases[m_Counter]);
            GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
            m_Counter++;

            m_Done = true;
            m_NameObject = "";
        }
    }

    public void ResetMirrorDay()
    {
        m_NameObject = "Mirarse";
        m_Done = false;
    }
}
