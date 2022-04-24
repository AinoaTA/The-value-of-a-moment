using System.Collections;
using UnityEngine;


public class Bed : Interactables
{
    public Camera cam;
    public GameObject m_SheetBad;
    public GameObject m_Sheet;  //sabana
    public GameObject bedText;
    private bool gameInitialized = false;
    Vector3 initPosBadSheet;
    float minDesplacement;
    float maxDesplacement = 2.17f;
    private float zWorldCoord;
    private float mOffset;

    private void Start()
    {
        options = 2;
        GameManager.GetManager().Bed = this;

        m_SheetBad.SetActive(true);
        initPosBadSheet = m_SheetBad.transform.position;
        minDesplacement = m_SheetBad.transform.position.x;
    }

    void OnMouseDrag()
    {
        // if (gameInitialized && !isDone)
        // {
        //     if (Input.GetAxisRaw("Mouse X") < 0)
        //     {
        //         if (m_SheetBad.transform.position.z < maxDesplacement)
        //         {
        //             m_SheetBad.transform.position += new Vector3(0, 0, 0.003f);
        //         }
        //         else if ((m_SheetBad.transform.position.z >= maxDesplacement))
        //             m_SheetBad.transform.position = new Vector3(m_SheetBad.transform.position.x, m_SheetBad.transform.position.y,maxDesplacement);
        //     }
        // }

        if (gameInitialized && !m_Done)
        {
            //print(m_SheetBad.transform.position.x);
            float movement = m_SheetBad.transform.position.x;
            float displacement = GetMouseXaxisAsWorldPoint() + mOffset;
            //print(displacement);
            if (displacement < minDesplacement)
            {
                print("not enough");
                movement = minDesplacement;
            }

            else if (displacement < maxDesplacement)
                movement = displacement;

            else if (displacement > maxDesplacement)
            {
                movement = maxDesplacement;
                m_Done = true;

            }
            m_SheetBad.transform.position = new Vector3(movement, m_SheetBad.transform.position.y, m_SheetBad.transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (m_Done)
            BedDone();
    }

    void OnMouseDown()
    {
        zWorldCoord = Camera.main.WorldToScreenPoint(m_SheetBad.transform.position).z;
        // offset = World pos - Mouse World pos
        mOffset = m_SheetBad.transform.position.y - GetMouseXaxisAsWorldPoint();
    }

    public void BedDone()
    {
        m_Done = true;
        cam.cullingMask = -1;
        //Cambiamos la sabana u objeto cama.
        m_Sheet.SetActive(true);
        m_SheetBad.SetActive(false);
        bedText.SetActive(false);
        GameManager.GetManager().PlayerController.ExitInteractable();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().CanvasManager.Lock();

        GameManager.GetManager().Autocontrol.AddAutoControl(m_MinAutoControl);
    }

    public void ResetBed()
    {
        GameManager.GetManager().Alarm.SetAlarmActive();
        GameManager.GetManager().Alarm.ResetTime();
        m_Done = false;
        m_Sheet.SetActive(false);
        m_SheetBad.SetActive(true);
        m_SheetBad.transform.position = initPosBadSheet;
        bedText.SetActive(true);
        gameInitialized = false;
    }
    public override void Interaction(int options)
    {
        switch (options)
        {
            case 1:
                if (!m_Done)
                {
                    GameManager.GetManager().PlayerController.SetInteractable("Bed");
                    gameInitialized = true;
                    GameManager.GetManager().CanvasManager.UnLock();
                    GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
                    cam.cullingMask = 7 << 0;
                }
                break;
            case 2:
                GameManager.GetManager().CanvasManager.FadeIn();
                GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
                GameManager.GetManager().Dialogue.StopDialogue();
                StartCoroutine(DelayReset());
                break;
            default:
                break;
        }
    }

    private float GetMouseXaxisAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zWorldCoord; // set z coord

        return Camera.main.ScreenToWorldPoint(mousePoint).x;
    }

    private IEnumerator DelayReset()
    {
        GameManager.GetManager().SoundController.QuitMusic();
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().PlayerController.SetInteractable("Alarm");
        GameManager.GetManager().PlayerController.PlayerSleepPos();
        GameManager.GetManager().Dialogue.StopDialogue();
        GameManager.GetManager().Window.ResetWindow();
        //GameManager.GetManager().Book.ResetInteractable();
        //GameManager.GetManager().Mirror.ResetInteractable();
        //GameManager.GetManager().VR.ResetVRDay();

        for (int i = 0; i < GameManager.GetManager().plants.Count; i++)
        {
            GameManager.GetManager().plants[i].NextDay();
            GameManager.GetManager().plants[i].ResetInteractable();
        }
        //no borrar hasta que estén tooooooodas las animaciones colocadas aquí.
        Debug.Log("NO FORGET: actions to reset.");
        ResetBed();
    }

    public override void ExitInteraction()
    {
        cam.cullingMask = -1;
        base.ExitInteraction();

    }
}
