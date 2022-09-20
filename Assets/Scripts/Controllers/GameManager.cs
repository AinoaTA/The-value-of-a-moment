//namespaces project
using Calendar;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager GetManager() => gameManager;
    #region gets sets
    public GameStateController gameStateController { get; set; }
    public CanvasController canvasController { get; set; }
    public NotificationController notificationController { get; set; }
    public Autocontrol autocontrol { get; set; }
    public SoundController soundController { get; set; }
    public ProgramMinigameController programMinigame { get; set; }
    public PlayerController playerController { get; set; }
    public MobileController mobile { get; set; }
    public CalendarController calendarController { get; set; }
    public EmailController emailController { get; set; }
    public InventoryTrashUI trashInventory { get; set; }
    public bool waterCanGrabbed { get; set; }
    public DayNightCycle dayNightCycle { get; set; }
    public Mobile mobileReal { get; set; }
    public Cinemachine.CinemachineStateDrivenCamera stateDriven { get; set; }
    public SceneLoader sceneLoader { get; set; }
    public CameraController cameraController { get; set; }
    public LevelData levelData { get; set; }
    public InterctableManager interactableManager { get; set; }
    public PlayerHandleInputs playerInputs { get; set; }
    public ActionObjectManager actionObjectManager { get; set; }
    public Alarm alarm { get; set; }

    //special references, maybe temproal- temporal mis ocjones
    public Computer computer { get; set; }
    #endregion
    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManager != this)
            Destroy(gameObject);
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
        StartCoroutine(EndMiniGameRoutine());
    }

    IEnumerator EndMiniGameRoutine()
    {
        cameraController.ExitInteractCam();
        canvasController.Lock();
        yield return new WaitForSeconds(0.25f);
        gameStateController.ChangeGameState(1);
    }
    #endregion
}