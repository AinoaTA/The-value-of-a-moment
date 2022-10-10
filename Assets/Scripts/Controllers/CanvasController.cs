using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject Pointer;

    private void Awake()
    {
        GameManager.GetManager().canvasController = this;
        Lock();
        // Debug.Log("there is a Lock() commented here"); 
    }

    public void ComputerScreenOut()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().canvasController.Lock();
    }
    public void ComputerScreenIn()
    {
        GameManager.GetManager().cameraController.StartInteractCam(5);
        GameManager.GetManager().canvasController.UnLock();
    }

    #region mouse pointer state
    public void Lock(bool showPointer = true, bool showMouse = false)
    {
        Pointer.SetActive(showPointer);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = showMouse;
    }

    public void UnLock()
    {
        Pointer.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    #endregion

    #region ModifyCanvasGroup
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
    #endregion
}
