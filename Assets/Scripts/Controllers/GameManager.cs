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
    public LayerMask m_WallMask;
    private Camera cam;
    [SerializeField] private float m_Distance = 30f;
    public GameObject textHelp;
    private Vector3 helpOffset = new Vector3(-10, 30, 0);

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currInteractable != null)
            {
                currInteractable.HideCanvas();
                currInteractable.Interaction();
                currInteractable = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerController.ExitInteractable();
            currInteractable = null;
        }

        Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
        {
            currInteractable = l_Hit.collider.gameObject.GetComponent<Interactables>();
            if (m_WallMask == (m_WallMask | (1 << l_Hit.collider.gameObject.layer)))
                PlayerController.WallPoint = l_Hit.point;


                if (currInteractable != null)
                {
                if ( !currInteractable.GetDone())
                {
                    lookingInteractable = currInteractable;
                    currInteractable.ShowCanvas();
                }
                else //if (currInteractable.GetDone())
                {
                    print("2");
                    lookingInteractable = currInteractable;
                    currInteractable.HideCanvas();
                    currInteractable = null;
                }
            }else if (currInteractable==null && lookingInteractable != null)
            {
                print("+3");
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
