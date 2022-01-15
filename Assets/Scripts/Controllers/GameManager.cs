using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum StateGame
    {
        Init = 0, //momento desperar-posponer
        GamePlay, //una vez despertado y moviendose por el nivel
        MiniGame //se ha iniciado un minigame
    }

    public StateGame m_CurrentStateGame;

    public LayerMask m_LayerMask;
    private Camera cam;
    private float m_Distance = 70f;
    public GameObject textHelp;
    private Vector3 helpOffset = new Vector3(-10, 30, 0);

    static GameManager m_GameManager;

    CanvasController m_CanvasController;
    NotificationController m_NotificationController;
    Autocontrol m_Autocontrol;

    Bed m_Bed;
    Window m_Window;
    Book m_Book;
    Alarm m_Alarm;
    DialogueControl m_Dialogue;


    public void SetCanvas(CanvasController canvas)
    {
        m_CanvasController = canvas;
    }
    public void SetNotificationController(NotificationController notification)
    {
        m_NotificationController = notification;
    }

    public void SetAutocontrol(Autocontrol _Autocontrol)
    {
        m_Autocontrol = _Autocontrol;
    }

    public void SetBed(Bed _bed)
    {
        m_Bed = _bed;
    }

    public void SetAlarm(Alarm _Alarm)
    {
        m_Alarm = _Alarm;
    }

    public void SetWindow(Window _Window)
    {
        m_Window = _Window;
    }

    public void SetDialogueControll(DialogueControl dialogue)
    {
        m_Dialogue = dialogue;
    }

    public void SetBook(Book _book)
    {
        m_Book = _book;
    }
    public static GameManager GetManager() => m_GameManager;
    public CanvasController GetCanvasManager() => m_CanvasController;
    public Autocontrol GetAutoControl() => m_Autocontrol;
    public NotificationController GetNotificationController() => m_NotificationController;
    public Bed GetBed() => m_Bed;
    public Window GetWindow() => m_Window;
    public Alarm GetAlarm() => m_Alarm;
    public DialogueControl GetDialogueControl() => m_Dialogue;

    public Book GetBook() => m_Book;
    private void Awake()
    {
        m_GameManager = this;
        cam = Camera.main;
        m_CurrentStateGame = StateGame.Init;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && m_CurrentStateGame == StateGame.GamePlay)
        {
            Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
                if (l_Hit.collider.GetComponent<Iinteract>() != null)
                    l_Hit.collider.GetComponent<Iinteract>().Interaction();
        }
        HelpText();
    }

    private void HelpText()
    {
        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        textHelp.transform.position = Input.mousePosition + helpOffset;
        TMP_Text text = textHelp.GetComponent<TMP_Text>();

        if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask) && !m_CanvasController.ScreenActivated() && !m_Alarm.GetIsActive() && m_CurrentStateGame == StateGame.GamePlay)
        {
            if (l_Hit.collider.GetComponent<Iinteract>() != null)//da error si se cambia el objeto de la cama así q mejor pongo esto
                text.text = l_Hit.collider.GetComponent<Iinteract>().NameAction();
        }
        else
            text.text = "";

        textHelp.GetComponent<TMP_Text>().text = text.text;

    }
}
