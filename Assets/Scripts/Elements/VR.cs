using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : Interactables
{
    private int m_Counter = 0;

    private void Awake()
    {
        GameManager.GetManager().VR = this;
    }
    public override void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_HelpPhrasesVoiceOff.Length-1)
                m_Counter = 0;

            GameManager.GetManager().Dialogue.SetDialogue(m_InteractPhrases[m_Counter]);
            GameManager.GetManager().Autocontrol.AddAutoControl(3);
            m_Counter++;

            m_Done = true;
        }

    }

    public void ResetVRDay()
    {
        m_Done = false;
    }

}
