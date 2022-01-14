using System.Collections;
using System.Collections.Generic;
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
    public bool m_GameActive=false;
    
    private void Awake()
    {
        cam = Camera.main;
        initPos = m_Sheet.transform.position;
        finalLimit = m_Limit.transform.position;
        finalLimit -= new Vector3(260, 0, 0);
        print("final "+finalLimit);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            MinigameCompleted();

        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0) && Input.GetAxisRaw("Mouse X")>0 && !m_Completed)
        {
           
            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_LayerMask))
            {
                //float AxisX = Input.GetAxisRaw("Mouse X") + m_Speed * Time.deltaTime;
                m_Sheet.transform.position+= new Vector3(5, 0, 0);
                print(m_Sheet.transform.position.x);

                if (m_Sheet.transform.position.x >= finalLimit.x)
                    MinigameCompleted();
            }
            
        }
    }

    private void MinigameCompleted()
    {
        m_Completed = true;
        //cualquier cosa que el jgador entienda que la hizo bien
        print("completed?");

        StartCoroutine(DelayCompleted());
        //GameManager.GetManager().GetBed().BedDone();
        //GameManager.GetManager().GetCanvasManager().DesctiveBedCanvas();
        //GameManager.GetManager().GetAutoControl().AddAutoControl(ValueConfident);
    }

    private IEnumerator DelayCompleted()
    {
        yield return new WaitForSeconds(1f);
        GameManager.GetManager().GetBed().BedDone();
        GameManager.GetManager().GetCanvasManager().DesctiveBedCanvas();
        GameManager.GetManager().GetAutoControl().AddAutoControl(ValueConfident);

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
        m_Sheet.transform.position = initPos;
        m_Completed = false;
    }
}
