using UnityEngine;
using System.Collections;
using Cinemachine;
namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public CanvasGroup pause;
        // [SerializeField] private CinemachineVirtualCamera virtualCamera3D;
        private bool paused = false;

        private void OnDisable()
        {
            GameManager.GetManager().playerInputs._PauseGame -= PauseGame;
        }

        private void Start()
        {
            GameManager.GetManager().playerInputs._PauseGame += PauseGame;
            GameManager.GetManager().canvasController.HideCanvas(pause);
        }

        public void PauseGame()
        {
            print("Hola");
            if (!paused)
            {
                print("paused");
                GameManager.GetManager().canvasController.UnLock();
                // virtualCamera3D.enabled = false;
                GameManager.GetManager().canvasController.ShowCanvas(pause);
                paused = true;
                //StartCoroutine(WaitToPauseGame());
                Time.timeScale = 0;
            }
            else
            {
                print("resume");
                ResumeGame();
            }

        }

        public void ResumeGame()
        {
            // virtualCamera3D.enabled = true;
            Time.timeScale = 1;
            GameManager.GetManager().canvasController.Lock();
            GameManager.GetManager().canvasController.HideCanvas(pause);
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
