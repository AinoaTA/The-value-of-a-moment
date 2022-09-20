using System.Collections;
using UnityEngine;

public class EmailController : MonoBehaviour
{
    [SerializeField] GameObject emailCanvas;
    [HideInInspector] public Email mail;
    public void Start()
    {
        GameManager.GetManager().emailController = this;
    }

    public void ShowEmail(bool v)
    {
        emailCanvas.SetActive(v);
    }

    public void Recieve() { StartCoroutine(RecieveMail()); }

    IEnumerator RecieveMail() 
    {
        yield return new WaitForSeconds(4f);
        print("waitiring");
        mail.MailRecieved();
    }
}
