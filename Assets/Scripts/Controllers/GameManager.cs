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
    public Autocontrol autocontrol { get; set; }
    public ProgramMinigameController programMinigame { get; set; }
    public PlayerController playerController { get; set; }
    public MobileController mobile { get; set; }
    public CalendarController calendarController { get; set; }
    public EmailController emailController { get; set; }
    public InventoryTrashUI trashInventory { get; set; }
    public bool waterCanGrabbed { get; set; }
    public DayController dayController { get; set; }
    public Mobile mobileReal { get; set; }
    public Cinemachine.CinemachineStateDrivenCamera stateDriven { get; set; }
    public SceneLoader sceneLoader { get; set; }
    public CameraController cameraController { get; set; }
    public LevelData levelData { get; set; }
    public InteractableManager interactableManager { get; set; }
    public PlayerHandleInputs playerInputs { get; set; }
    public GeneralActionsManager actionObjectManager { get; set; }
    public Alarm alarm { get; set; }
    public DialogueManager dialogueManager { get; set; }
    public BlockController blockController { get; set; }

    //special references, maybe temproal- temporal mis ocjones
    public Computer computer { get; set; }
    #endregion

    public bool programmedInteractableDone = false;
    public int realizedInteractables;
    public bool programmed, alexVisited, checkAida;
    public GameObject diaDos;

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

    public void ToActive()
    {
        diaDos.SetActive(false);
        switch (dayController.GetDayNumber())
        {
            case DayController.Day.one:
                break;
            case DayController.Day.two:
                diaDos.SetActive(true);
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }

    }

    public void IncrementInteractableCount()
    {
        realizedInteractables++;
        if(realizedInteractables >= 5)
        {
            int currentTime = (int)dayController.GetTimeDay();
            if (currentTime >= 3) // Es de noche
            {
                blockController.BlockAll(true);
                blockController.Unlock("Ventanas");
                blockController.Unlock("Bed");
                if (programmedInteractableDone)
                {
                    GameManager.GetManager().dialogueManager.SetDialogue("D2Procrast");
                }
                StartCoroutine(Timbre("D2Timbre"));
                StartCoroutine(Timbre("D2TimbreOp1"));
                StartCoroutine(Llaves());
            }
            else // Cambio de hora
            {
                dayController.ChangeDay(++currentTime);
                realizedInteractables = 0;
            }
        }
    }

    public void ResetInteractable()
    {
        realizedInteractables = 0;
    }

    IEnumerator Timbre(string dialogue)
    {
        yield return new WaitForSecondsRealtime(4f);
        // TODO: Hacer sonar el timbre

        yield return new WaitForSecondsRealtime(4f);
        GameManager.GetManager().dialogueManager.SetDialogue(dialogue);
    }

    IEnumerator Llaves()
    {
        yield return new WaitForSecondsRealtime(4f);
        // TODO: Que suenen las llaves

        yield return new WaitForSecondsRealtime(4f);
        GameManager.GetManager().dialogueManager.SetDialogue("D2MundoAida");

        yield return new WaitForSecondsRealtime(4f);
        GameManager.GetManager().dialogueManager.SetDialogue("D2Epilogod2");

        yield return new WaitForSecondsRealtime(4f);
        // TODO: Sonido de cerrar la puerta

    }
    #endregion
}