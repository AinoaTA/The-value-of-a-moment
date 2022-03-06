using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : Interactables, IntfInteract
{
    private string m_NameObject = "Mirar VR";
    private bool m_Done;
    private int m_Counter = 0;
    public float distance;

    public string[] m_MirrorInteractPhrases;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
    private void Awake()
    {
        GameManager.GetManager().VR = this;
    }
    public void Interaction()
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

    public string NameAction()
    {
        return m_NameObject;
    }

    public bool GetDone()
    {
        return m_Done;
    }
    public void ResetVRDay()
    {
        m_NameObject = "Mirar VR";
        m_Done = false;
    }

    public string[] GetPhrases()
    {
        throw new System.NotImplementedException();
    }

    public float GetDistance()
    {
        return distance;
    }
}
