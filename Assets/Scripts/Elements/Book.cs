using System.Collections;
using UnityEngine;

public class Book : Interactables
{
    private int m_Counter = 0;
    public string[] m_BookInteractPhrases;
    public Grabbing m_Grabbing;

    public delegate void DelegateSFXBook();
    public static DelegateSFXBook m_DelegateSFXBook;

    private void Start()
    {
        GameManager.GetManager().Book = this;
        m_InteractPhrases = m_BookInteractPhrases;
    }

    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if(m_Grabbing != null)
                {
                    m_Grabbing.SetAccessCamera(true);
                    GameManager.GetManager().PlayerController.SetInteractable("Grab");
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
                    HideCanvas();

                    if (m_Counter >= m_InteractPhrases.Length)
                        m_Counter = 0;

                    StartCoroutine(DelayDialogue());
                }
                break;
        }
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(1f);
        GameManager.GetManager().Dialogue.SetDialogue(m_InteractPhrases[m_Counter]);
        m_DelegateSFXBook?.Invoke();
        m_Counter++;
        yield return new WaitForSeconds(3f);
        GameManager.GetManager().Dialogue.StopDialogue();
    }
}
