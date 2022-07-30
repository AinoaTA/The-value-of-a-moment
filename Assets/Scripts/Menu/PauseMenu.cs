using UnityEngine;
using System.Collections;
using Cinemachine;
namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private CinemachineVirtualCamera virtualCamera3D;

        private bool paused = false;

        private void Start()
        {
            pauseCanvas.SetActive(false);
        }

        private void Update()
        {
            //if (!paused && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.GamePlay
            //    && Input.GetKeyDown(KeyCode.P) && !GameManager.GetManager().CanvasManager.m_activated)
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
            GameManager.GetManager().canvasController.UnLock();
            virtualCamera3D.enabled = false;
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            StartCoroutine(WaitToPauseGame());
        }

        public void ResumeGame()
        {
            virtualCamera3D.enabled = true;
            Time.timeScale = 1;
            GameManager.GetManager().canvasController.Lock();
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
}
