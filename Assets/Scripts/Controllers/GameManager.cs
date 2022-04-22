using UnityEngine;

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
    private Camera cam;
    [SerializeField] private float m_Distance = 30f;
    public GameObject textHelp;
    private Vector3 helpOffset = new Vector3(-10, 30, 0);

    private Interactables currInteractable;
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
    public FirstMinigameController FirstMinigame { get; set; }
    public Mirror Mirror { get; set; }
    public VR VR { get; set; }
    public PlayerController PlayerController { get; set; }
    public Plant[] plants { get; set; }

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

        if (lookingInteractable != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Active Interaction");

                currInteractable.HideCanvas();
                currInteractable.Interaction();

                lookingInteractable = null;

            }
            //else if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    //Debug.Log("Quit interactable canvas");
            //    //currInteractable.HideCanvas();
            //    //currInteractable = null;
            //}
        }

        Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(l_Ray, out RaycastHit l_Hit_, m_Distance, m_LayerMask))
        {
            currInteractable = l_Hit_.collider.gameObject.GetComponent<Interactables>();

            if (currInteractable != null)
            {
                if (!currInteractable.GetDone() && Input.GetMouseButtonDown(0))
                {
                    lookingInteractable = currInteractable;
                    lookingInteractable.ShowCanvas();
                }
            }
            else if (currInteractable != lookingInteractable && lookingInteractable != null)
            {
                print("no alwais");
                lookingInteractable.HideCanvas();
                lookingInteractable = null;
            }
        }
    }

    public void TurnOnComputer()
    {
        PlayerController.SetInteractable("Computer");
    }

    public void OpenDoor()
    {
        door.SetTrigger("Open");
    }
}
