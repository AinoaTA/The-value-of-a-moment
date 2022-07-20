using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager GetManager() => gameManager;

    public GameStateController gameStateController { get; set; }
    public CanvasController CanvasManager { get; set; }
    public NotificationController NotificationController { get; set; }
    public Autocontrol Autocontrol { get; set; }
    public SoundController SoundController { get; set; }
    public FirstMinigameController ProgramMinigame { get; set; }
    public PlayerController PlayerController { get; set; }
    public MobileController mobile { get; set; }
    public CalendarController calendarController { get; set; }
    public InventoryTrash trashInventory { get; set; }
    public bool WaterCanGrabbed { get; set; }
    public DayNightCycle dayNightCycle { get; set; }
    public Mobile mobileReal { get; set; }
    public Cinemachine.CinemachineStateDrivenCamera stateDriven { get; set; }
    public SceneLoader sceneLoader { get; set; }
    public CameraController cameraController { get; set; }
    public LevelData levelData { get; set; }
    public InterctableManager interactableManager { get; set; }
    public PlayerHandleInputs playerInputs { get; set; }
    public ActionObjectManager actionObjectManager { get; set; }

    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }


    //public void ResetTrash()
    //{ 
    //    foreach(var trash in trashes)
    //    {
    //        trash.ResetInteractable();
    //    }
    //    bucket.ResetInteractable();
    //    InventoryTrash.ResetInventory();
    //}


    public void TurnOnComputer()
    {
        cameraController.StartInteractCam(6);
    }

    public void OpenDoor()
    {
      // door.SetTrigger("Open");
    }

    #region SetStateGames
    /// <summary>
    /// Lock(), ExitInteractable and ChangeState
    /// </summary>
    /// <param name="state"></param>
    public void StartThirdPersonCamera()
    {
        //PlayerController.ExitInteractable();
        CanvasManager.Lock();
        //ChangeGameState(StateGame.GamePlay);
        StartCoroutine(EndMiniGameRoutine());
    }

    IEnumerator EndMiniGameRoutine()
    {
        cameraController.ExitInteractCam();
        CanvasManager.Lock();
        yield return new WaitForSeconds(0.25f);
        gameStateController.ChangeGameState(1);
    }
    #endregion
}