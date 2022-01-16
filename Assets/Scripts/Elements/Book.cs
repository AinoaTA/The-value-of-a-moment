using UnityEngine;

public class Book : MonoBehaviour, Iinteract
{
    private string m_NameObject = "Leer libro";
    private bool m_Done;
    private int m_Counter = 0;

    public string[] m_HelpPhrases;
    public string[] m_BookInteractPhrases;

    public delegate void DelegateSFXBook();
    public static DelegateSFXBook m_DelegateSFXBook;
    private void Awake()
    {
        GameManager.GetManager().SetBook(this);
    }
    public void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_BookInteractPhrases.Length)
                m_Counter = 0;

            GameManager.GetManager().GetDialogueControl().SetDialogue(m_BookInteractPhrases[m_Counter]);
            m_DelegateSFXBook?.Invoke();
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

    public string[] GetPhrases()
    {
        return m_HelpPhrases;
    }

    public void ResetBookDay()
    {
        m_NameObject = "Continuar leyendo";
        m_Done = false;
    }
}
