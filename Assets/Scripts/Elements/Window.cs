using UnityEngine;

public class Window : MonoBehaviour, Iinteract
{
    private bool m_Done;
    private Vector3 m_ClosePos;
    public GameObject m_Glass;

    public Transform m_OpenPos;
    public WindowMinigame m_miniGame;
    public string[] m_HelpPhrases;
    public float distance; 
    
    [HideInInspector] public string m_NameObject = "Abrir ventana";
    private void Awake()
    {
        GameManager.GetManager().SetWindow(this);
    }
    private void Start()
    {
        m_ClosePos = m_Glass.transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    

    public void Interaction()
    {
        if (!m_Done)
        {
            //inicia minijuego
            m_miniGame.m_GameActive = true;
            GameManager.GetManager().GetCanvasManager().ActiveWindowCanvas();
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
        }
    }


    public void WindowDone()
    {
        m_Done = true;
        m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, m_OpenPos.transform.position.y,m_Glass.transform.position.z);
        GameManager.GetManager().GetAutoControl().AddAutoControl(5);
        m_NameObject = "";
        //Cambiamos la sábana u objeto cama.
    }
    public void ResetWindow()
    {
        m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, m_ClosePos.y, m_Glass.transform.position.z);
        m_NameObject = "Abrir ventana";
        m_Done = false;
    }

    public bool GetIsCompleted()
    {
        return m_Done;
    }

    public string NameAction()
    {
        return m_NameObject;
    }

    public bool GetDone()
    {
        return m_Done;
    }

    public string[] GetPhrases()
    {
        return m_HelpPhrases;
    }

    public float GetDistance()
    {
        return distance;
    }
}
