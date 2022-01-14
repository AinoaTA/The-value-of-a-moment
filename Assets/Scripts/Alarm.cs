using UnityEngine;

public class Alarm : MonoBehaviour
{
    private float m_Timer;
    [SerializeField]private float m_MaxTime;
    private bool m_Alarm=true;
    public GameObject CanvasAlarm;


    private void Awake()
    {
        GameManager.GetManager().SetAlarm(this);
    }
    private void Update()
    {
        if(m_Alarm)
        m_Timer += Time.deltaTime;

        if (m_Timer > m_MaxTime)
            StartAlarm();
    }
    private void StartAlarm()
    {
        CanvasAlarm.SetActive(true);
        m_Timer=0;
        //sonid
    }

    public void WakeUp()
    {
        m_Alarm = false;
        ResetTime();

        /// GameManager.GetManager().GetCanvasManager().FadeInSolo();
        //animacion player se levanta
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        Debug.Log("Animación player se levanta");
        CanvasAlarm.SetActive(false);
    }
    public void StillSleeping()
    {
        m_Alarm = true;
        ResetTime();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
        CanvasAlarm.SetActive(false);
    }

    public bool GetIsActive()
    {
        return m_Alarm;
    }

    public void SetAlarmActive()
    {
        m_Alarm=true;
    }

    public void ResetTime()
    {
        m_Timer = 0;
        m_MaxTime = 10;
    }
}
