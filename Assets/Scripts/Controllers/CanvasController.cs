using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject FadeInScreen;
    public GameObject ComputerScreen;
    public GameObject m_NotificationCanvas;
    public GameObject NotificationMessage;
    public GameObject MessageOpen;
    public GameObject WindowCanvas;
    public GameObject FirstMinigameCanvas;

    public GameObject Pointer;

    public bool m_activated;
    private void Start()
    {
        GameManager.GetManager().CanvasManager = this;
        Lock();
        Debug.Log("there is a Lock() commented here");
        m_activated = false;
        Pointer.SetActive(false);
    }
    public void FadeInComputer()
    {
        FadeInScreen.SetActive(true);
        StartCoroutine(DelayFadeComputer());
    }

    public void ComputerScreenOut()
    {
        FirstMinigameCanvas.SetActive(false);
        // FirstMinigameCanvas.GetComponent<FirstMinigameController>().re
        // GameManager.GetManager().ProgramMinigame.
        GameManager.GetManager().StartThirdPersonCamera();
        m_activated = false;
        CloseWindow();
    }
    public void ComputerScreenIn()
    {
        GameManager.GetManager().cameraController.StartInteractCam(5);
        GameManager.GetManager().CanvasManager.UnLock();
       // GameManager.GetManager().Autocontrol.ShowAutocontroler(0);
       // GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
        m_activated = true;
        StartCoroutine(DelayFadeComputer());

    }
    private IEnumerator DelayFadeComputer()
    {
        yield return new WaitForSeconds(0.5f);
        ComputerScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        //si existe notificacion...
        yield return new WaitForSeconds(0.4f);
        if (GameManager.GetManager().NotificationController.m_CurrentNotRead)
            NotificationMessage.SetActive(true);
    }

    public void CloseWindow()
    {
        m_activated = false;
        GameManager.GetManager().CanvasManager.Lock();
        StartCoroutine(DelayFadeClose());
    }

    private IEnumerator DelayFadeClose()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        yield return new WaitForSeconds(0.5f);
        ComputerScreen.SetActive(false);
        //if (!GameManager.GetManager().NotificationController.m_CurrentNotRead)
        //{
        //    m_NotificationCanvas.SetActive(false);
        //}
       // GameManager.GetManager().Autocontrol.ShowAutocontroler(1);
        //NotificationMessage.SetActive(false);
        //MessageOpen.SetActive(false);
        //GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
    }

    public void FadeIn()
    {
        FadeInScreen.SetActive(true);
        StartCoroutine(DelayFadeIn());
    }

    private IEnumerator DelayFadeIn()
    {
        yield return new WaitForSeconds(3f);
        FadeInScreen.SetActive(false);
    }
    public bool ScreenActivated()
    {
        return m_activated;
    }

    public void StartMinigame()
    {
        if (!GameManager.GetManager().ProgramMinigame.GetSolved())
        {
            GameManager.GetManager().ProgramMinigame.m_started = true;
           // GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
            FirstMinigameCanvas.SetActive(true);
        }
    }

    public void FinishMiniGame()
    {
        FirstMinigameCanvas.SetActive(false);
    }

    public void Lock()
    {
        Pointer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnLock()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Pointer.SetActive(false);
    }

}
