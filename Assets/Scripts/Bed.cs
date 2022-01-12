using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour,Iinteract
{
    [HideInInspector]public string m_NameObject;
    private bool m_WakeUp, m_Sleep;

    private void Start()
    {
        m_Sleep = true;
        m_WakeUp = false;
        SetWakeUp();
    }

    private void SetWakeUp()
    {
        m_NameObject = "Despertar";
        m_Sleep = false;
        m_WakeUp = true;

    }
    private void SetSleep()
    {
        m_NameObject = "Dormir";
        m_Sleep = true;
        m_WakeUp = false;
        
    }
    private void SetTidy()
    {
        m_NameObject = "Recoger";
        m_Sleep = false;
        m_WakeUp = false;

    }
    private void SetNone()
    {
        m_NameObject = "";
        
    }

    public void Interaction()
    {
        if (m_Sleep)
            SetWakeUp();
        else if (m_WakeUp)
            SetTidy();
        else
            SetSleep();
    }

    public string NameAction()
    {
        return m_NameObject;
    }

}
