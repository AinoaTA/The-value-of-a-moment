using UnityEngine;

public class Alarm : MonoBehaviour
{
    private float m_Timer;
    public bool forceDebugMode = true; //REMOVER ESTO UNA VEZ LA BUIDL ESTE DONE
    [SerializeField] private float m_MaxTime;
    private bool m_Alarm = true;
    public GameObject CanvasAlarm;

    public ButtonTrigger WakeUpTrigger;
    public string[] m_phrases;
    private int m_count;
    public delegate void DelegateSFX();
    public static DelegateSFX m_DelegateSFX;

    private void Awake()
    {
        GameManager.GetManager().Alarm = this;
    }

    private void Start()
    {
        if (forceDebugMode)
        {
            ForceWakeUP();
        }
    }
    private void Update()
    {

        if (!forceDebugMode)
        {
            if (m_Alarm)
                m_Timer += Time.deltaTime;

            if (m_Timer > m_MaxTime)
                StartAlarm();
        }
    }

    private void ForceWakeUP()
    {

        m_Alarm = false;
        //ResetTime();
        GameManager.GetManager().Dialogue.SetTimer();
        ///GameManager.GetManager().GetCanvasManager().FadeInSolo();
        //animacion player se levanta
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().PlayerController.PlayerWakeUpPos();
        GameManager.GetManager().SoundController.SetMusic();
        CanvasAlarm.SetActive(false);
    }

    private void StartAlarm()
    {
        GameManager.GetManager().SoundController.QuitMusic();
        CanvasAlarm.SetActive(true);
        m_DelegateSFX?.Invoke();
        m_Timer = 0;
        //sonid
    }

    public void WakeUp()
    {
        if (WakeUpTrigger.m_Counter > 3)
        {
            m_Alarm = false;
            ResetTime();

            GameManager.GetManager().Dialogue.SetTimer();
            ///GameManager.GetManager().GetCanvasManager().FadeInSolo();
            //animacion player se levanta
            GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
            GameManager.GetManager().PlayerController.PlayerWakeUpPos();
            GameManager.GetManager().SoundController.SetMusic();
            CanvasAlarm.SetActive(false);
        }
        else if (WakeUpTrigger.m_Counter <= 3)
        {
            if (WakeUpTrigger.m_Counter > 1)
                GameManager.GetManager().Dialogue.SetDialogue("5 Minutos más...");

            WakeUpTrigger.LessEscaleWakeUp();
        }

    }
    public void StillSleeping()
    {
        if (m_count >= m_phrases.Length)
            m_count = 0;

        m_Alarm = true;
        ResetTime();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
        CanvasAlarm.SetActive(false);
        GameManager.GetManager().Autocontrol.RemoveAutoControl(5);
        GameManager.GetManager().Dialogue.SetDialogue(m_phrases[m_count]);
        m_count++;
    }

    public bool GetIsActive()
    {
        return m_Alarm;
    }

    public void SetAlarmActive()
    {
        m_Alarm = true;
    }

    public void ResetTime()
    {
        GameManager.GetManager().SoundController.StopSound();
        m_Timer = 0;
        m_MaxTime = 10;
    }
}
