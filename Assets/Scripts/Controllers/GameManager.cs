using TMPro;
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
    [SerializeField]private float m_Distance = 30f;
    public GameObject textHelp;
    private Vector3 helpOffset = new Vector3(-10, 30, 0);
    
    private Interactables curr;

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

    public static GameManager GetManager() => m_GameManager;

    private void Awake()
    {
        m_GameManager = this;
        cam = Camera.main;
        m_CurrentStateGame = StateGame.GamePlay;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_CurrentStateGame == StateGame.GamePlay)
        {
            Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
            {
                if (l_Hit.collider.GetComponent<Interactables>() != null)
                {
                    Interactables curr = l_Hit.collider.GetComponent<Interactables>();
                    if (!curr.GetDone())
                    {
                        print(curr);
                        curr.ShowCanvas();
                    }
                    //PlayerController.SetInteractable(curr.tag);
                    //GameManager.GetManager().GetPlayer().ActiveMovement(l_Hit.collider.gameObject);
                }
            }
        }
      //  InteractCanvas();
    }



    //private void InteractCanvas()
    //{
    //    Ray l_Ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    //textHelp.transform.position = Input.mousePosition + helpOffset;
    //    //  TMP_Text text = textHelp.GetComponent<TMP_Text>();

    //    Interactables interact = null;
    //    if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask) && !CanvasManager.ScreenActivated() && !Alarm.GetIsActive() && m_CurrentStateGame == StateGame.GamePlay)
    //    {
    //        interact = l_Hit.collider.GetComponent<Interactables>();

    //        if (interact != null) //da error si se cambia el objeto de la cama asi q mejor pongo esto
    //        {
    //            interact.ShowCanvas();
    //        }
    //    }
    //}

    public void TurnOnComputer()
    {
        PlayerController.SetInteractable("Computer");
    }
}
