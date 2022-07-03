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
    //private void Awake()
    //{
    //    stateDriven = FindObjectOfType<Cinemachine.CinemachineStateDrivenCamera>();
    //}
    //private void Start()
    //{
    //   // cam = Camera.main;
    //    //m_CurrentStateGame = StateGame.Init;
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (currInteractable != null && currInteractable.showing)
    //        {
    //            currInteractable.HideCanvas();
    //            currInteractable.Interaction(1);
    //            currInteractable = null;

    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Q) && currInteractable != null && currInteractable.options > 1)
    //    {
    //        if (currInteractable != null)
    //        {
    //            currInteractable.HideCanvas();
    //            currInteractable.Interaction(2);
    //            currInteractable = null;
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        print("is it here??");
    //        PlayerController.ExitInteractable();
    //        m_CurrentStateGame = StateGame.GamePlay;
    //        currInteractable = null;
    //    }
    //}

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