using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Email : MonoBehaviour
{
    [HideInInspector] public bool blockWarning;
    [HideInInspector] public EmailAnswer currentAnswerOpen;
    public GameObject emailContent, plusContent, enviar;
    [SerializeField] string companyName;
    [SerializeField] string emailDir;
    [SerializeField] string title;
    [SerializeField] TMP_Text[] paragraphs;
    [SerializeField] List<string> stringsParagraphs = new List<string>();
    [SerializeField] TMP_Text companyN, emailD, titleText;

    
    public GameObject warning;
    float autocontrolEarned;
    bool sent=false;

    [SerializeField] EmailAnswer[] autocontrolAnswer;
    private void Start()
    {
        for (int i = 0; i < paragraphs.Length; i++)
            stringsParagraphs.Add(paragraphs[i].text.ToString());

    }
    public void OpenEmail()
    {
        if (blockWarning) return;
        GameManager.GetManager().emailController.mail = this;
        emailContent.SetActive(true);

        if (sent) return;
        companyN.text = companyName;
        emailD.text = emailDir;
        titleText.text = title;
    }

    public void Warning()
    {
        warning.gameObject.SetActive(true);
        blockWarning = true;
    }

    public void CloseWarning()
    {
        warning.gameObject.SetActive(false);
        blockWarning = false;

    }
    public void SendEmail()
    {
        warning.SetActive(false);
        sent = true;
        enviar.SetActive(false);
        plusContent.SetActive(false);
        for (int i = 0; i < autocontrolAnswer.Length; i++)
            autocontrolEarned += autocontrolAnswer[i].autocontrolSave;

        GameManager.GetManager().autocontrol.AddAutoControl(autocontrolEarned);
    }

    public void SetTextParagraph(int id, string answer)
    {
        paragraphs[id].text = stringsParagraphs[id] + " " + answer;
    }
}
