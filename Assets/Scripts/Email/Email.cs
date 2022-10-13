using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Email : MonoBehaviour
{
    [HideInInspector] public bool blockWarning;
    [HideInInspector] public EmailAnswer currentAnswerOpen;
    [SerializeField] bool isWrote;
    [Header("Contact Details")]
    [SerializeField] string companyName;
    [SerializeField] string emailDir;
    [SerializeField] string title;

    [Header("Writting Part")]
    public GameObject emailContent;
    public GameObject plusContent;
    public GameObject enviar;
    public GameObject writtingContent;
    [SerializeField] TMP_Text[] paragraphs;
    [SerializeField] List<string> stringsParagraphs = new List<string>();
    [SerializeField] TMP_Text companyN;
    [SerializeField] TMP_Text emailD;
    [SerializeField] TMP_Text titleText;

    public GameObject warning;
    float autocontrolEarned;
    bool sent = false;

    [SerializeField] EmailAnswer[] autocontrolAnswer;

    [Header("Reading Part")]
    [SerializeField] GameObject mailRecieve;
    [SerializeField] TMP_Text[] paragraphsRecieve;
    [SerializeField] MailRecieve[] mailsRecieve;
    [SerializeField] GameObject notification;
    [SerializeField] DayController.Day day;
    private void Start()
    {
        notification.SetActive(false);

        for (int i = 0; i < paragraphs.Length; i++)
            stringsParagraphs.Add(paragraphs[i].text.ToString());

    }
    public int one;
    public void OpenEmail()
    {
        if (blockWarning || day != GameManager.GetManager().dayController.GetDayNumber()) return;

        //GameManager.GetManager().emailController.mail = this;

        GameManager.GetManager().emailController.CloseOthers(this);
        emailContent.SetActive(true);
        if (isWrote)
        {
            mailRecieve.SetActive(true);
            writtingContent.SetActive(false);
        }
        else
        {
            if (recieved) mailRecieve.SetActive(true);
            else writtingContent.SetActive(true);

            notification.SetActive(false);

            if (sent) return;
            companyN.text = companyName;
            emailD.text = emailDir;
            titleText.text = title;
        }
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
        sent = true;
        blockWarning = false;
        warning.SetActive(false);
        enviar.SetActive(false);
        plusContent.SetActive(false);
        emailContent.SetActive(false);
        for (int i = 0; i < autocontrolAnswer.Length; i++)
            autocontrolEarned += autocontrolAnswer[i].autocontrolSave;

        GameManager.GetManager().autocontrol.AddAutoControl(autocontrolEarned);
    }

    public void SetTextParagraph(int id, string answer)
    {
        paragraphs[id].text = stringsParagraphs[id] + " " + answer;
    }

    MailRecieve mail;
    bool recieved;
    public void MailRecieved()
    {
        if (!sent) return;
        writtingContent.SetActive(false);
        notification.SetActive(true);
        recieved = true;
        mail = autocontrolEarned < 10 ? mail = mailsRecieve[0] : mail = mailsRecieve[1];
        for (int i = 0; i < mail.messagesParagraph.Length; i++)
            paragraphsRecieve[i].text = mail.messagesParagraph[i];
    }

    public void Close() 
    {
        writtingContent.SetActive(false);
        emailContent.SetActive(false);
    }
}
