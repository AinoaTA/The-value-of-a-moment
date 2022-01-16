using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class DialogueControl : MonoBehaviour
{
    private TMP_Text m_Text;
    private float m_TimerShowDialogue =0;
    private float m_MaxTimeShowDialogue=5; //dependiendo de la longitud de la frase.
    private bool m_DialogueActive;

    private int maxInt;
     //frases de ayuda.
   
    //private List<GameObject> m_ListInteract=new List<GameObject>();

    private float m_Timer=-100;
    public void SetTimer()
    {
        m_Timer = 0;
    }

    private void Awake()
    {
        GameManager.GetManager().SetDialogueControll(this);
        m_Text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        //m_ListInteract.Add(GameManager.GetManager().GetBed().gameObject);
        //m_ListInteract.Add(GameManager.GetManager().GetWindow().gameObject);
        //m_ListInteract.Add(GameManager.GetManager().GetBook().gameObject);

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
        if (m_Timer > 12)
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
        //for (int i = 0; i < m_ListInteract.Count; i++)
        //{
        //    if (!m_ListInteract[i].GetComponent<Iinteract>().GetDone())
        //    {
        //        int random = Random.Range(0, maxInt);
        //        SetDialogue(m_ListInteract[i].GetComponent<Iinteract>().GetPhrases()[random]);
        //    }
        //}
    }

}
