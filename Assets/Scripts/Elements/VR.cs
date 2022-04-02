using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : Interactables
{
    private int m_Counter = 0;

    public string[] m_MirrorInteractPhrases;


    private void Awake()
    {
        GameManager.GetManager().VR = this;
    }
    public override void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_MirrorInteractPhrases.Length)
                m_Counter = 0;

            GameManager.GetManager().Dialogue.SetDialogue(m_MirrorInteractPhrases[m_Counter]);
            GameManager.GetManager().Autocontrol.AddAutoControl(3);
            m_Counter++;

            m_Done = true;
            m_NameObject = "";
        }

    }

    public void ResetVRDay()
    {
        m_NameObject = "Mirar VR";
        m_Done = false;
    }

}
