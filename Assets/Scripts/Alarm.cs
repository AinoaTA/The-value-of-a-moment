using UnityEngine;

public class Alarm : MonoBehaviour
{
    private float m_Timer;
    [SerializeField]private float m_MaxTime;
    private bool m_Alarm;
    public GameObject CanvasAlarm;


    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > m_MaxTime)
            m_Alarm = true;

        if (m_Alarm)
            StartAlarm();
    }
    private void StartAlarm()
    {
        CanvasAlarm.SetActive(true);

        //sonid
    }

    public void WakeUp()
    {
        ResetTime();
        CanvasAlarm.SetActive(false);
        GameManager.GetManager().GetCanvasManager().FadeInSolo();
        //animacion player se levanta

    }
    public void StillSleeping()
    {
        ResetTime();
        CanvasAlarm.SetActive(false);
    }

    private void ResetTime()
    {
        m_Alarm = false;
        m_Timer = 0;
        m_MaxTime = 10;
    }
}
