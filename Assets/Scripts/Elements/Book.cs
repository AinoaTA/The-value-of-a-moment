using UnityEngine;

public class Book : Interactables
{
    private int m_Counter = 0;
    public string[] m_BookInteractPhrases;

    public delegate void DelegateSFXBook();
    public static DelegateSFXBook m_DelegateSFXBook;

    private void Awake()
    {
        GameManager.GetManager().Book = this;
        m_UsePhrases = m_BookInteractPhrases;
    }

    public override void Interaction()
    {
        if (!m_Done)
        {
            if (m_Counter >= m_UsePhrases.Length)
                m_Counter = 0;

            GameManager.GetManager().Dialogue.SetDialogue(m_UsePhrases[m_Counter]);
            m_DelegateSFXBook?.Invoke();
            m_Counter++;

            m_Done = true;
            m_NameObject = "";
        }
        
    }
}
