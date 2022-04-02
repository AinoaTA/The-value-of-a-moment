using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class DialogueControl : MonoBehaviour
{
    private TMP_Text m_Text;
    private float m_TimerShowDialogue =0;
    private float m_MaxTimeShowDialogue=10; //dependiendo de la longitud de la frase.
    private bool m_DialogueActive;

    private int maxInt=2;

    [SerializeField]private List<GameObject> m_ListInteract = new List<GameObject>();

    private float m_Timer=-100;
    public void SetTimer()
    {
        m_Timer = 0;
        m_TimerShowDialogue = 0;
    }

    private void Awake()
    {
        GameManager.GetManager().Dialogue = this;
        m_Text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        m_ListInteract.Add(GameManager.GetManager().Bed.gameObject);
        m_ListInteract.Add(GameManager.GetManager().Book.gameObject);
    }

    private void Update()
    {
        if (m_DialogueActive)
        {
            m_TimerShowDialogue += Time.deltaTime;
            if (m_TimerShowDialogue > m_MaxTimeShowDialogue)
            {
                m_DialogueActive = false;
                m_Text.text = "";
                m_TimerShowDialogue = 0;
            }             
        }

        m_Timer += Time.deltaTime;
        //print(m_Timer);
        if (m_Timer > 12 && !m_DialogueActive && GameManager.GetManager().m_CurrentStateGame==GameManager.StateGame.GamePlay)
            HelpDialogue();
    }
    public void SetDialogue(string dialogue)
    {
        SetTimer();
        m_DialogueActive = true;
        m_Text.text = dialogue;
    }

    private void HelpDialogue()
    {
        for (int i = 0; i < m_ListInteract.Count; i++)
        {
            Interactables l_interactable = m_ListInteract[i].GetComponent<Interactables>();

            if (!l_interactable.GetDone())
            {
                int random = Random.Range(0, maxInt);
                SetDialogue(l_interactable.GetPhrases()[random]);
            }
        }
    }
}
