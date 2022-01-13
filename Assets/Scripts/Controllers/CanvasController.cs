using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    
    public GameObject FadeInScreen;
    public GameObject ComputerScreen;
    public GameObject m_NotificationCanvas;
    public GameObject NotificationMessage;
    public GameObject MessageOpen;
    public GameObject BedCanvas;


    private bool m_activated;
    private void Awake()
    {
        GameManager.GetManager().SetCanvas(this);
        m_activated=false;
    }
    public void FadeInComputer()
    {
        FadeInScreen.SetActive(true);
        StartCoroutine(DelayFadeComputer());
    }

    public void ComputerScreenIn()
    {
        m_activated = true;
        FadeInComputer();
        FadeInScreen.SetActive(true);
        StartCoroutine(DelayFadeComputer());
        
    }
    private IEnumerator DelayFadeComputer()
    {
        
        yield return new WaitForSeconds(0.5f);
        ComputerScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FadeInScreen.SetActive(false);

        //si existe notificacion...
        yield return new WaitForSeconds(0.4f);
        if (GameManager.GetManager().GetNotificationController().m_CurrentNotRead)
           NotificationMessage.SetActive(true);
       

    }

    public void CloseWindow()
    {
        m_activated = false;
        StartCoroutine(DelayFadeClose());

    }

    private IEnumerator DelayFadeClose()
    {
        FadeInComputer();
        yield return new WaitForSeconds(0.5f);
        ComputerScreen.SetActive(false);
        if (!GameManager.GetManager().GetNotificationController().m_CurrentNotRead)
        {
            m_NotificationCanvas.SetActive(false);
        }
        NotificationMessage.SetActive(false);
        MessageOpen.SetActive(false);
       
    }


    public void ActiveBedCanvas()
    {
        //FadeIn();
        BedCanvas.SetActive(true);
    }

    public void DesctiveBedCanvas()
    {
        
        BedCanvas.SetActive(false);
    }
    //FADE IN SIN NADA
    public void FadeIn()
    {
        FadeInScreen.SetActive(true);
        StartCoroutine(DelayFadeIn());
    }

    private IEnumerator DelayFadeIn()
    {
        yield return new WaitForSeconds(0.7f);
        FadeInScreen.SetActive(false);
    }
    public bool ScreenActivated()
    {
        return m_activated;
    }
}
