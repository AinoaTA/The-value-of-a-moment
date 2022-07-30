using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject ComputerScreen;
    public GameObject m_NotificationCanvas;
    public GameObject NotificationMessage;
    public GameObject MessageOpen;
    public GameObject WindowCanvas;
    public GameObject programMinigame;

    public GameObject Pointer;
    
    private void Start()
    {
        GameManager.GetManager().canvasController = this;
        Lock();
       //Debug.Log("there is a Lock() commented here");
        Pointer.SetActive(false);
    }

    public void ComputerScreenOut()
    {
        GameManager.GetManager().StartThirdPersonCamera();
       
        GameManager.GetManager().canvasController.Lock();
        StartCoroutine(DelayFadeClose());
    }
    public void ComputerScreenIn()
    {
        GameManager.GetManager().cameraController.StartInteractCam(5);
        GameManager.GetManager().canvasController.UnLock();
    }
    private IEnumerator DelayFadeClose()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        yield return new WaitForSeconds(0.5f);
        ComputerScreen.SetActive(false);
    }
    #region mouse pointer state
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
    #endregion

    public void ShowCanvas(CanvasGroup can) 
    {
        can.alpha = 1;
        can.blocksRaycasts = true;
        can.interactable = true;
    }
    public void HideCanvas(CanvasGroup can) 
    {
        can.alpha = 0;
        can.blocksRaycasts = false;
        can.interactable = false;

    }
}
