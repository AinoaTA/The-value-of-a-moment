using System.Collections;
using UnityEngine;

public class BedMinigame : MonoBehaviour
{

    private int ValueConfident=5; //
    private Camera cam;
    private bool m_Completed;
    private Vector3 finalLimit;
    private Vector3 initPos;
    public GameObject m_Sheet;
    public GameObject m_Limit;
    public LayerMask m_LayerMask;
    [HideInInspector]public bool m_GameActive=false;
    
    private void Start()
    {
        cam = Camera.main;
        initPos = m_Sheet.transform.position;
        finalLimit = m_Limit.transform.position;
        finalLimit += new Vector3(0, 0, 0);
    }
    private void Update()
    {
        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0) && Input.GetAxisRaw("Mouse X")>0 && !m_Completed)
        {
           
            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_LayerMask))
            {
                m_Sheet.transform.position+= new Vector3(5, 0, 0);
                if (m_Sheet.transform.position.x >= finalLimit.x)
                    MinigameCompleted();
            }
            
        }
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
        //GameManager.GetManager().Bed.BedDone();
       // GameManager.GetManager().CanvasManager.DesctiveBedCanvas();
        GameManager.GetManager().Autocontrol.AddAutoControl(ValueConfident);

          GameManager.GetManager().gameStateController.ChangeGameState(1);

        m_GameActive = false;
        ResetMinigame();
        
    } 

    public bool GameActive()
    {
        return m_GameActive;
    }


    private void ResetMinigame()
    {
        m_Sheet.transform.position = initPos;
        m_Completed = false;
    }
}
