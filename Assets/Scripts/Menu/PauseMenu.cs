using UnityEngine;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CinemachineVirtualCamera virtualCamera3D;
    public float pausedScale = 0;
    public float resumeScale = 1;

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.GetManager().m_CurrentStateGame != GameManager.StateGame.MiniGame)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        virtualCamera3D.enabled = false;
        Time.timeScale = pausedScale;
        GameManager.GetManager().CanvasManager.UnLock();
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        virtualCamera3D.enabled = true;
        Time.timeScale = resumeScale;
        GameManager.GetManager().CanvasManager.Lock();

        pauseCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
