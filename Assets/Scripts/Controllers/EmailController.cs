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

    //provisional
    public void Recieve() { mail.MailRecieved(); }//StartCoroutine(RecieveMail()); }

    IEnumerator RecieveMail() 
    {
        Debug.LogWarning("Provisional Method");
        yield return new WaitForSeconds(4f);
        
       
    }
}
