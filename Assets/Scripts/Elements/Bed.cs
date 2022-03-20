using System.Collections;
using UnityEngine;

public class Bed : Interactables,IntfInteract
{
    private bool m_Done;
    public GameObject m_SheetBad;
    public GameObject m_Sheet;//sábana
    [HideInInspector]public string m_NameObject;
    public string[] m_HelpPhrases;
    public BedMinigame m_miniGame;
    [SerializeField] private float distance;

    private void Awake()
    {
        GameManager.GetManager().Bed = this;
    }
    private void Start()
    {
        m_SheetBad.SetActive(true);
        m_NameObject = "Hacer la cama";
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public void BedDone()
    {
        m_Done = true;
        //Cambiamos la sábana u objeto cama.
        m_Sheet.SetActive(true);
        m_SheetBad.SetActive(false);
        //
        m_NameObject = "Dormir";
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
    }
    public void Interaction()
    {
        if (!m_Done)
        {
            //inicia minijuego
            m_miniGame.m_GameActive = true;
            GameManager.GetManager().CanvasManager.ActiveBedCanvas();
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
        GameManager.GetManager().Book.ResetBookDay();
        GameManager.GetManager().Mirror.ResetMirrorDay();
        GameManager.GetManager().VR.ResetVRDay();
        ResetBed();
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
