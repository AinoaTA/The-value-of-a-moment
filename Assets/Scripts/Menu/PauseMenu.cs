using UnityEngine;
using System.Collections;
using Cinemachine;
namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public CanvasGroup pause;
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
            if (!paused)
            {
                paused = true;
                GameManager.GetManager().canvasController.UnLock();
                GameManager.GetManager().cameraController.Block3DMovement(!paused);
                GameManager.GetManager().canvasController.ShowCanvas(pause);
              
                Time.timeScale = 0;
            }
            else
                ResumeGame();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            paused = false;
            GameManager.GetManager().canvasController.Lock();
            GameManager.GetManager().canvasController.HideCanvas(pause);
            GameManager.GetManager().cameraController.Block3DMovement(!paused);
        }

        public void QuitGame()
        {
            Time.timeScale = 1;
            GameManager.GetManager().gameStateController.ChangeGameState(0);
            GameManager.GetManager().sceneLoader.LoadLevel(0);
        }
    }
}
//FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Pause", transform.position);