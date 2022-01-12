using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public LayerMask m_LayerMask;
    private Camera cam;
    private float m_Distance = 70f;
    public GameObject textHelp;
    private Vector3 helpOffset = new Vector3(-10, 30,0);
    //public GameObject m_Particles; PARA VISUALIZAR D�NDE TOCAMOs

    static GameManager m_GameManager;

    CanvasController m_CanvasController;
    NotificationController m_NotificationController;
    Autocontrol m_Autocontrol;
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
    
    public static GameManager GetManager() => m_GameManager;
    public CanvasController GetCanvasManager() => m_CanvasController;
    public Autocontrol GetAutoControl() => m_Autocontrol;
    public NotificationController GetNotificationController() =>m_NotificationController;

    private void Awake()
    {
        m_GameManager = this;
        cam = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask))
                l_Hit.collider.GetComponent<Iinteract>().Interaction();
        }
        HelpText();
    }

    private void HelpText()
    {
        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        textHelp.transform.position =Input.mousePosition + helpOffset;
        TMP_Text text= textHelp.GetComponent<TMP_Text>();
        if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_Distance, m_LayerMask) && !m_CanvasController.ScreenActivated())
        {
            text.text = l_Hit.collider.GetComponent<Iinteract>().NameAction();
        }else
            text.text = "";

        textHelp.GetComponent<TMP_Text>().text = text.text;

    }
}
