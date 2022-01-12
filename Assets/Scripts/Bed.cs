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
    }

    private void SetSleep()
    {
        m_NameObject = "Dormir";
    }
    private void SetTidy()
    {
        m_NameObject = "Recoger";

    }

    public void Interaction()
    {
        //if (m_Sleep)
        //    SetWakeUp();
        //else if (m_WakeUp)
        //    SetTidy();
        //else
        //    SetSleep();
    }

    public string NameAction()
    {
        return m_NameObject;
    }

}
