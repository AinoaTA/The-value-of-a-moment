using UnityEngine;
namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public CanvasGroup pause;
        private bool paused = false;
        public FMODMusic MusicGameplay;
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Pause");
            if (!paused)
            {
                paused = true;
                GameManager.GetManager().canvasController.UnLock(false, true);
                GameManager.GetManager().cameraController.Block3DMovement(!paused);
                GameManager.GetManager().canvasController.ShowCanvas(pause);
                MusicGameplay.Pause(1f);

                Time.timeScale = 0;
            }
            else
                ResumeGame();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            paused = false;

            if (GameManager.GetManager().gameStateController.CheckGameState(0)) GameManager.GetManager().canvasController.Lock(false, false);
            else  GameManager.GetManager().canvasController.Lock(true, false);

            GameManager.GetManager().canvasController.HideCanvas(pause);
            GameManager.GetManager().cameraController.Block3DMovement(!paused);
            MusicGameplay.Pause(0f);
        }

        public void QuitGame()
        {
            Time.timeScale = 1;
            GameManager.GetManager().gameStateController.ChangeGameState(0);
            GameManager.GetManager().sceneLoader.LoadLevel(0);
        }

    }
}