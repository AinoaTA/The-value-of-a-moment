using UnityEngine;

public class Window : Interactables, IntfInteract
{
    public GameObject m_Glass;
    public string[] m_HelpPhrases;
    
    private float mOffset;
    private float zWorldCoord;
    private float minHeight;
    private float maxHeight = 7.35f;
    private bool isOpen = false;
    private bool gameInitialized = false;

    private string m_NameObject = "Abrir ventana";
    public float distance;

    private void Awake()
    {
        GameManager.GetManager().Window = this;
        minHeight = m_Glass.transform.position.y;
    }

    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_Glass.transform.position).z;

        // offset = World pos - Mouse World pos
        mOffset = m_Glass.transform.position.y - GetMouseYaxisAsWorldPoint();
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isOpen)
        {
            float height = m_Glass.transform.position.y;
            float displacement = GetMouseYaxisAsWorldPoint() + mOffset;

            if (displacement < minHeight)
                height = minHeight;

            else if (displacement < maxHeight)
                height = displacement;

            else if (displacement > maxHeight)
            {
                height = maxHeight;
                isOpen = true;
            }

            m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, height, m_Glass.transform.position.z);
        }
    }
    
    private float GetMouseYaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).y;
    }

    #region Interface Interact methods

    public void Interaction()
    {
        if (!isOpen)
            gameInitialized = true; // Inicia minijuego
    }

    public bool GetDone()
    {
        return isOpen;
    }

    public string NameAction()
    {
        return m_NameObject;
    }

    public string[] GetPhrases()
    {
        return m_HelpPhrases;
    }

    public float GetDistance()
    {
        return distance;
    }

    public void ResetWindow()
    {
        isOpen = false;
        gameInitialized = false;
        m_Glass.transform.position = new Vector3(m_Glass.transform.position.x, minHeight, m_Glass.transform.position.z);
    }

    #endregion
}
