using UnityEngine;

public class EmailController : MonoBehaviour
{
    [SerializeField] GameObject emailCanvas;
    public Email mail;
    public Email[] mails;
    public void Start()
    {
        GameManager.GetManager().emailController = this;
    }

    public void ShowEmail(bool v)
    {
        emailCanvas.SetActive(v);
        GameManager.GetManager().computer.ComputerON();
    }

    public void CloseOthers(Email e)
    {
        if (e == mail) return;
        mail = e;
        for (int i = 0; i < mails.Length; i++)
        {
            if (mail != mails[i])
                mails[i].Close();
        }
    }

    public void Recieve() { mail.MailRecieved(); }
}
