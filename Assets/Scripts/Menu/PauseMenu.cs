using UnityEngine;
using System.Collections;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CinemachineVirtualCamera virtualCamera3D;

    private bool paused = false;

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    private void Update()
    {
        //if (!paused && GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.GamePlay
        //    && Input.GetKeyDown(KeyCode.P) && !GameManager.GetManager().CanvasManager.activated)
        //{
        //    PauseGame();
        //}

        //if(paused && Input.GetKeyDown(KeyCode.P))
        //{
        //    ResumeGame();
        //}
    }

    public void PauseGame()
    {
        GameManager.GetManager().CanvasManager.UnLock();
        virtualCamera3D.enabled = false;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        StartCoroutine(WaitToPauseGame());
    }

    public void ResumeGame()
    {
        virtualCamera3D.enabled = true;
        Time.timeScale = 1;
        GameManager.GetManager().CanvasManager.Lock();
        pauseCanvas.SetActive(false);
        paused = false;
    }

    public void QuitGame()
    {
        GameManager.GetManager().gameStateController.ChangeGameState(0);
        Time.timeScale = 1;
        GameManager.GetManager().sceneLoader.LoadLevel(0);
        //Application.Quit();
    }
    
    private IEnumerator WaitToPauseGame()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        paused = true;
    }

}
