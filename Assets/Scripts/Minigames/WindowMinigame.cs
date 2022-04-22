using System.Collections;
using UnityEngine;

public class WindowMinigame : MonoBehaviour
{

    private Camera cam;
    private bool m_Completed;
    private Vector3 finalLimit;
    private Vector3 initPos;
    public GameObject m_Glass;
    public GameObject m_Limit;
    public LayerMask m_LayerMask;

    [HideInInspector]public bool m_GameActive = false;
    [SerializeField]private float m_Speed = 0.3f;
    private Vector3 LastLeft;

    private void Start()
    {
        cam = Camera.main;
        initPos = m_Glass.transform.position;
        finalLimit = m_Limit.transform.position;
        finalLimit += new Vector3(0, 70, 0);
    }
    private void Update()
    {
        if (!Input.GetMouseButton(0) && !m_Completed)
            m_Glass.transform.position = Vector3.Lerp(m_Glass.transform.position, initPos, m_Speed * Time.deltaTime);

        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0) && Input.GetAxisRaw("Mouse Y") > 0 && !m_Completed)
        {
            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_LayerMask))
            {
                m_Glass.transform.position += new Vector3(0, 2.5f, 0);

                LastLeft = m_Glass.transform.position;
                LastLeft.x = 0;
                if (m_Glass.transform.position.y >= finalLimit.y)
                    MinigameCompleted();
            }
        }

        print(m_Glass.transform.position.y + "aaaaa" + finalLimit.y);
    }

    private void MinigameCompleted()
    {
        m_Completed = true;
        //cualquier cosa que el jgador entienda que la hizo bien

        StartCoroutine(DelayCompleted());
    }

    private IEnumerator DelayCompleted()
    {
        yield return new WaitForSeconds(1f);
        GameManager.GetManager().Window.GetDone();
        GameManager.GetManager().CanvasManager.DesctiveWindowCanvas();

        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;

        m_GameActive = false;
        ResetMinigame();
    }

    public bool GameActive()
    {
        return m_GameActive;
    }


    private void ResetMinigame()
    {
        m_Glass.transform.position = initPos;
        m_Completed = false;
    }
}
