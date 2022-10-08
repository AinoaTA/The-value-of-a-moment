using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public string[] m_EllePhrases;
    public VoiceOff[] m_WakeUpVoice;
    public GameObject CanvasAlarm;

    public float m_Autocontrol;

    [SerializeField] private float m_MaxTime;
    private bool m_Alarm = true;
    private int m_count;
    [SerializeField] private float m_Timer;
    private bool m_AlarmON;


    //public delegate void DelegateSFX();
    //public static DelegateSFX m_DelegateSFX;

    private static FMOD.Studio.EventInstance alarmsfx;
    private static FMOD.Studio.EventInstance inbed;

    private void Start()
    {
        GameManager.GetManager().alarm = this;
        alarmsfx = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Alarm");
        inbed = FMODUnity.RuntimeManager.CreateInstance("event:/Elle/GetInBed");

        GameManager.GetManager().cameraController.StartInteractCam(1);
        CanvasAlarm.SetActive(false);
        StartCoroutine(StartDayDelay());

        GameManager.GetManager().playerInputs._FirstInteraction += StartDay;
        GameManager.GetManager().playerInputs._SecondInteraction += BackDay;
    }

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction -= StartDay;
        GameManager.GetManager().playerInputs._SecondInteraction -= BackDay;
    }

    private void Update()
    {
        if (m_Alarm && !m_AlarmON)
            m_Timer += Time.deltaTime;

        if ((m_Timer > m_MaxTime) && !m_AlarmON &&!started)
            StartAlarm();
    }

    private void StartDay()
    {
        if (m_AlarmON && GameManager.GetManager().gameStateController.CheckGameState(0))
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
        if (m_AlarmON && GameManager.GetManager().gameStateController.CheckGameState(0))
        {
            StillSleeping();
        }
    }
    bool started;
    private void StartAlarm()
    {
        started = true;
        alarmsfx.start();
        alarmsfx.release();
        inbed.start();
        GameManager.GetManager().dialogueManager.StartDialogue("Alarm");

        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        CanvasAlarm.SetActive(true);
        m_Timer = 0;
        m_AlarmON = true;
    }
    public IEnumerator NormalWakeUp()
    {
        // GameManager.GetManager().PlayerController.SetInteractable("WakeUp");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/AlarmOff");
        GameManager.GetManager().cameraController.StartInteractCam(2);
        CanvasAlarm.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().playerController.PlayerWakeUpPos();
        GameManager.GetManager().canvasController.Lock(true);
        m_Alarm = false;
        ResetTime();
    }

    public void StillSleeping()
    {
        m_AlarmON = false;

        if (m_count >= m_EllePhrases.Length)
            m_count = 0;

        m_Alarm = true;
        ResetTime();
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().gameStateController.ChangeGameState(0);
        GameManager.GetManager().autocontrol.RemoveAutoControl(m_Autocontrol);
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
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        inbed.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        m_AlarmON = false;
        m_Timer = 0;
    }

    private IEnumerator StartDayDelay()
    {
        GameManager.GetManager().canvasController.Lock(false);
        yield return new WaitForSeconds(4);
    }
}
