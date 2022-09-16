using TMPro;
using UnityEngine;

public class Email : MonoBehaviour
{
    public GameObject emailContent;
    public string companyName;
    public string emailDir;
    public string title;

    public TMP_Text companyN, emailD, titleText;

    [HideInInspector] public EmailAnswer currentAnswerOpen;
    public GameObject warining;
    public bool blockWarning;
    bool sent;
    private void Start()
    {
        GameManager.GetManager().email = this;
    }
    public void OpenEmail()
    {
        if (blockWarning) return;
        emailContent.SetActive(true);

        companyN.text = companyName;
        emailD.text = emailDir;
        titleText.text = title;
    }

    public void Warning()
    {
        warining.gameObject.SetActive(true);
        blockWarning = true;
    }

    public void CloseWarning()
    {
        warining.gameObject.SetActive(false);
        blockWarning = false;

    }
    public void SendEmail()
    {


    }
}
