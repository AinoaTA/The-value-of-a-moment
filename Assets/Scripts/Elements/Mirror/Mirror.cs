using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour,Iinteract
{
    private string m_NameObject = "Mirarse";
    private bool m_Done;
    private int m_Counter = 0;

    public string[] m_MirrorInteractPhrases;

    private void Awake()
    {
        GameManager.GetManager().SetMirror(this);
    }
    public void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_MirrorInteractPhrases.Length)
                m_Counter = 0;

            GameManager.GetManager().GetDialogueControl().SetDialogue(m_MirrorInteractPhrases[m_Counter]);
            GameManager.GetManager().GetAutoControl().RemoveAutoControl(5);
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
    public void ResetMirrorDay()
    {
        m_NameObject = "Mirarse";
        m_Done = false;
    }

    public string[] GetPhrases()
    {
        throw new System.NotImplementedException();
    }
}
