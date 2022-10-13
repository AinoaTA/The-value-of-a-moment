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
    int one;
    public void ShowEmail(bool v)
    {
        if (one == 0)
        {
            one++;
            GameManager.GetManager().dayController.TaskDone();
        }
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

    public void ResetDay()
    {
        one = 0;
    }

    public void Recieve() { mail.MailRecieved(); }
}
