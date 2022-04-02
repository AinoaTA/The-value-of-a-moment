using System.Collections;
using UnityEngine;

public class Bed : Interactables
{
    public GameObject m_SheetBad;
    public GameObject m_Sheet;//sábana
    public BedMinigame m_miniGame;

    private bool isDone = false;
    private bool gameInitialized = false;
    float minDesplacement = -3.13f;
    float maxDesplacement =-3.069f;

    private void Awake()
    {
        GameManager.GetManager().Bed = this;
    }
    private void Start()
    {
        m_SheetBad.SetActive(true);
        m_NameObject = "Hacer la cama";
    }

    void OnMouseDrag()
    {
        if (gameInitialized && !isDone)
        {
            if (Input.GetAxisRaw("Mouse X") < 0)
            {
                if (m_SheetBad.transform.position.z < maxDesplacement)
                {
                    m_SheetBad.transform.position += new Vector3(0, 0, 0.003f);
                }
                else if ((m_SheetBad.transform.position.z >= maxDesplacement))
                    m_SheetBad.transform.position = new Vector3(m_SheetBad.transform.position.x, m_SheetBad.transform.position.y,maxDesplacement);
            }
        }
    }

    private void OnMouseUp()
    {
        if ((m_SheetBad.transform.position.z <= maxDesplacement) && (m_SheetBad.transform.position.z >= minDesplacement))
            BedDone();
    }
    public void BedDone()
    {
        isDone=m_Done = true;
        //Cambiamos la sábana u objeto cama.
        m_Sheet.SetActive(true);
        m_SheetBad.SetActive(false);
        //
        m_NameObject = "Dormir";
        GameManager.GetManager().PlayerController.ExitInteractable();
        GameManager.GetManager().Autocontrol.AddAutoControl(5);
    }

    public void ResetBed()
    {//para cuando se vuelve a dormir y despierta.
        GameManager.GetManager().Alarm.SetAlarmActive();
        GameManager.GetManager().Alarm.ResetTime();
        m_Done = false;
        m_Sheet.SetActive(false);
        m_SheetBad.SetActive(true);
        m_NameObject = "Hacer la cama";


        isDone = false;
        gameInitialized = false;
    }
    public override void Interaction()
    {
        m_Done = isDone;
        if (!m_Done)
        {
            gameInitialized = true;
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
        }
        else
        {
            GameManager.GetManager().CanvasManager.FadeIn();
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
            StartCoroutine(DelaySleep());
        }
    }

    private IEnumerator DelaySleep()
    {
        GameManager.GetManager().SoundController.QuitMusic();
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().PlayerController.PlayerSleepPos();
        GameManager.GetManager().Window.ResetWindow();
        GameManager.GetManager().Book.ResetInteractable();
        GameManager.GetManager().Mirror.ResetMirrorDay();
        GameManager.GetManager().VR.ResetVRDay();
        ResetBed();
    }
}
