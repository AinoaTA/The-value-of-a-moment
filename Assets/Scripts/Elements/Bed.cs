using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour,Iinteract
{
    private bool m_Done;
    public GameObject m_SheetBad;
    public GameObject m_Sheet;//sábana
    [HideInInspector]public string m_NameObject;

    public BedMinigame m_miniGame;
    private void Awake()
    {
        GameManager.GetManager().SetBed(this);
    }
    private void Start()
    {
        m_SheetBad.SetActive(true);
        m_NameObject = "Hacer la cama";
    }

    public void BedDone()
    {
        m_Done = true;
        m_Sheet.SetActive(true);
        m_SheetBad.SetActive(false);
        m_NameObject = "Dormir";
        GameManager.GetManager().GetAutoControl().AddAutoControl(5);
        //Cambiamos la sábana u objeto cama.
    }

    public void ResetBed()
    {//para cuando se vuelve a dormir y despierta.
        GameManager.GetManager().GetAlarm().SetAlarmActive();
        GameManager.GetManager().GetAlarm().ResetTime();
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
            GameManager.GetManager().GetCanvasManager().ActiveBedCanvas();
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.MiniGame;
        }
        else
        {
            GameManager.GetManager().GetCanvasManager().FadeIn();

            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
            ResetBed();
            Debug.Log("MOMENTO MIMIR....");
        }
            
    }

    public string NameAction()
    {

        return m_NameObject;
    }

}
