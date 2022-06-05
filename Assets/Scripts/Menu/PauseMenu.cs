using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CinemachineVirtualCamera virtualCamera3D;
    public float pausedScale = 0;
    public float resumeScale = 1;

    private bool paused = false;

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!paused && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay
            && Input.GetKeyDown(KeyCode.Escape) && !GameManager.GetManager().CanvasManager.m_activated)
        {
            PauseGame();
        }

        if(paused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        GameManager.GetManager().CanvasManager.UnLock();
        virtualCamera3D.enabled = false;
        Time.timeScale = pausedScale;
        pauseCanvas.SetActive(true);
        StartCoroutine(WaitToPauseGame());
    }

    public void ResumeGame()
    {
        virtualCamera3D.enabled = true;
        Time.timeScale = resumeScale;
        GameManager.GetManager().CanvasManager.Lock();
        pauseCanvas.SetActive(false);
        paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private IEnumerator WaitToPauseGame()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        paused = true;
    }

}
