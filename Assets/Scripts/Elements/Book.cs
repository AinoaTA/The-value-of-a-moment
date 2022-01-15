using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour, Iinteract
{
    private string m_NameObject = "Leer libro";
    private bool m_Done;

    public string[] m_HelpPhrases;

    private void Awake()
    {
        GameManager.GetManager().SetBook(this);
    }
    public void Interaction()
    {
        if (!m_Done)
            print("Leidisimo");
        
    }

    public void BookRead()
    {
        m_Done = true;
    }

    public string NameAction()
    {
        return m_NameObject;
    }

    public bool GetDone()
    {
        return m_Done;
    }

    public string[] GetPhrases()
    {
        return m_HelpPhrases;
    }
}
