using System;
using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public GameObject CanvasAlarm;

    public float m_Autocontrol;

    [SerializeField] private float maxTime;
    private bool alarm = true;
    private int counter;
    [SerializeField] private float timer;
    private bool alarmRinging;

    public FMODMusic MusicGameplay;

    private static FMOD.Studio.EventInstance alarmsfx;

    private void Start()
    {
        GameManager.GetManager().alarm = this;
        alarmsfx = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Alarm");
        

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
        if (alarm && !alarmRinging)
            timer += Time.deltaTime;

        if ((timer > maxTime) && !alarmRinging)
            StartAlarm();
    }

    private void StartDay()
    {
        if (alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0))
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
        if (alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0))
        {
            StillSleeping();
        }
    }
    private void StartAlarm()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                AlarmAndMood();
                if(counter>0)
                    Show();
                else
                    GameManager.GetManager().dialogueManager.SetDialogue("Alarm", delegate { Show(); });
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2Start", delegate
                {
                    AlarmAndMood();
                    Show();
                });
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private void AlarmAndMood()
    {
        alarmsfx.start();
        MusicGameplay.Mood(0f);
    }

    public IEnumerator NormalWakeUp()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                string name;
                if (counter == 0) name = "GetUp1";
                else name = "GetUp2";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    StartCoroutine(Delay());
                });
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1", delegate
                {
                    StartCoroutine(Delay());
                });
                break;
                
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/AlarmOff");
        MusicGameplay.Mood(1f);
        GameManager.GetManager().cameraController.StartInteractCam(2);
        CanvasAlarm.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().playerController.PlayerWakeUpPos();
        GameManager.GetManager().canvasController.Lock(true);
        alarm = false;
        ResetTime();

    }
    #region dialogues helps
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().dialogueManager.SetDialogue("Ventana", delegate 
        {
            GameManager.GetManager().blockController.Unlock("Ventanas");
        });
    }
    #endregion
    void Show()
    {
        CanvasAlarm.SetActive(true);
        timer = 0;
        alarmRinging = true;
    }
    public void StillSleeping()
    {
        alarmRinging = false;

        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                string name = "alarm";
                if (counter == 0) name = "Alarm2";
                else name = "Alarm3";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    StartAlarm();
                });

                if (counter >= 2)
                    StartAlarm();
                break;
            case DayController.Day.two:
                
                break;
                
        }
        
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.one)
        {
            
        }
        counter++;

        alarm = true;
        ResetTime();
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().gameStateController.ChangeGameState(0);
        GameManager.GetManager().autocontrol.RemoveAutoControl(m_Autocontrol);
    }

    public bool GetIsActive()
    {
        return alarm;
    }

    public void SetAlarmActive()
    {
        alarm = true;
    }

    public void ResetTime()
    {
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        alarmRinging = false;
        timer = 0;
    }

    private IEnumerator StartDayDelay()
    {
        GameManager.GetManager().canvasController.Lock(false);
        yield return new WaitForSeconds(4);
    }
}
