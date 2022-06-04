using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager m_GameManager;

    public enum StateGame
    {
        Init = 0,   // Momento despertar-posponer
        GamePlay,   // Una vez despertado y moviendose por el nivel
        MiniGame    // Se ha iniciado un minigame
    }
    public StateGame m_CurrentStateGame;

    public LayerMask m_LayerMask;
    public LayerMask m_WallMask;
    private Camera cam;
    [SerializeField] private float m_Distance = 30f;

    [SerializeField] private Interactables currInteractable;
    private Interactables lookingInteractable;

    public CanvasController CanvasManager { get; set; }
    public NotificationController NotificationController { get; set; }
    public Autocontrol Autocontrol { get; set; }
    public Bed Bed { get; set; }
    public Alarm Alarm { get; set; }
    public Window Window { get; set; }
    public DialogueControl Dialogue { get; set; }
    public Book Book { get; set; }
    public SoundController SoundController { get; set; }
    public FirstMinigameController ProgramMinigame { get; set; }
    public Mirror Mirror { get; set; }
    public VR VR { get; set; }
    public PlayerController PlayerController { get; set; }
    public InventoryTrash InventoryTrash { get; set; }
    public List<Plant> Plants = new List<Plant>();
    public MobileController mobile { get; set; }
    public CalendarController calendarController { get; set; }
    public Regadera WaterCan { get; set; }

    public bool WaterCanGrabbed { get; set; }
    public DayNightCycle dayNightCycle { get; set; }

    public Animator door;

    public static GameManager GetManager() => m_GameManager;

    private void Awake()
    {
        m_GameManager = this;
    }
    private void Start()
    {
        cam = Camera.main;
        m_CurrentStateGame = StateGame.Init;
    }

    private void Update()
    {
        if (m_CurrentStateGame != StateGame.GamePlay)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currInteractable != null && currInteractable.showing)
            {
                print("interactuando button");
                currInteractable.HideCanvas();
                currInteractable.Interaction(1);
                currInteractable = null;
                m_CurrentStateGame = StateGame.MiniGame;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currInteractable != null && currInteractable.options > 1)
        {
            if (currInteractable != null)
            {
                currInteractable.HideCanvas();
                currInteractable.Interaction(2);
                currInteractable = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerController.ExitInteractable();
            m_CurrentStateGame = StateGame.GamePlay;
            currInteractable = null;
        }

        Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
        {
            currInteractable = l_Hit.collider.gameObject.GetComponent<Interactables>();

            if (currInteractable != null && currInteractable!=lookingInteractable)
            {
                lookingInteractable = currInteractable;
                currInteractable.ShowCanvas();
            }
            else if (currInteractable == null && lookingInteractable != null)
            {
                lookingInteractable.HideCanvas();
                lookingInteractable = null;
            }
        }
    }

    public void ChangeGameState(StateGame state)
    {
        StartCoroutine(Delay(state));
    }
    private IEnumerator Delay(StateGame state)
    {
        yield return new WaitForSecondsRealtime(1);
        m_CurrentStateGame = state;
    }

    public void TurnOnComputer()
    {
        PlayerController.SetInteractable("Computer");
    }

    public void OpenDoor()
    {
        door.SetTrigger("Open");
    }


    public void EndMinigame()
    {
        PlayerController.ExitInteractable();
        CanvasManager.Lock();
        ChangeGameState(GameManager.StateGame.GamePlay);
    }
}
