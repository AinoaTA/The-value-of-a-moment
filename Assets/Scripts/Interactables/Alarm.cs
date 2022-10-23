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
    private bool canInteract;
    [SerializeField] private bool talking;
    public FMODMusic MusicGameplay;
    public GameObject eventAlex;
    private static FMOD.Studio.EventInstance alarmsfx;

    private void Start()
    {
        counter = 0;
        GameManager.GetManager().alarm = this;
        alarmsfx = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Alarm");
        GameManager.GetManager().cameraController.StartInteractCam(1);
        CanvasAlarm.SetActive(false);
        StartCoroutine(StartDayDelay());

        GameManager.GetManager().playerInputs._FirstInteraction += AfirmativoAlex;
        GameManager.GetManager().playerInputs._SecondInteraction += NegativeAlex;

        GameManager.GetManager().playerInputs._FirstInteraction += StartDay;
        GameManager.GetManager().playerInputs._SecondInteraction += BackDay;
    }

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs._FirstInteraction += AfirmativoAlex;
        GameManager.GetManager().playerInputs._SecondInteraction += NegativeAlex;

        GameManager.GetManager().playerInputs._FirstInteraction -= StartDay;
        GameManager.GetManager().playerInputs._SecondInteraction -= BackDay;
    }

    private void Update()
    {
        if (alarm && !alarmRinging && !talking)
            timer += Time.deltaTime;

        if ((timer > maxTime) && !alarmRinging)
            StartAlarm();
    }

    private void StartDay()
    {
        if (canInteract/*alarmRinging*/ && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
        if (canInteract/*alarmRinging*/ && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
        {
            StillSleeping();
        }
    }
    bool once;
    private void StartAlarm()
    {
        alarmRinging = true;
        talking = false;
        MusicGameplay.Mood(0f);
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                AlarmAndMood();

                if (counter > 0) Show();
                else GameManager.GetManager().dialogueManager.SetDialogue("Alarm", delegate { Show(); });

                break;
            case DayController.Day.two:
                if (counter > 0)
                {
                    AlarmAndMood();
                    Show();
                }
                else
                    GameManager.GetManager().dialogueManager.SetDialogue("D2Start", delegate
                    {
                        AlarmAndMood();
                        Show();
                    });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AlarmAndMood()
    {
        alarmsfx.start();
        talking = false;
        canInteract = true;
        print("start sound");
    }

    public IEnumerator NormalWakeUp()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                string name = counter == 0 ? "GetUp1" : "GetUp2";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    StartCoroutine(Delay());
                });
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1", delegate
                {
                    GameManager.GetManager().alexVisited = true;
                    GameManager.GetManager().counterAlex = true;

                    StartCoroutine(Delay2());
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();

        }
        canInteract = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/AlarmOff");
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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

    IEnumerator Delay2()
    {
        yield return new WaitWhile(() => GameManager.GetManager().dialogueManager.waitDialogue);
        GameManager.GetManager().dialogueManager.SetDialogue("Ventana", delegate
        {
            // TODO: unlock de los interactables
            GameManager.GetManager().UnlockBasicTasks();
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
        canInteract = false;
        talking = true;
        string name;
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                //string name = "alarm";
                name = counter == 0 ? "Alarm2" : "Alarm3";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    StartAlarm();
                });

                if (counter >= 2)
                    StartAlarm();
                break;
            case DayController.Day.two:

                if (counter == 0) name = "D2Alarm_Op2";
                else if (counter == 1) name = "D2Alarm_Op3";
                else name = "D2Alarm_Op3";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    print("=");
                    if (name == "D2Alarm_Op3" && !negated)
                    {
                        eventAlex.gameObject.SetActive(true);
                        unique = true;
                    }
                    else
                    {
                        print("lloro");
                        StartAlarm();
                    }
                }/*, forceInvoke: true*/);

                break;

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
        ResetTime();
    }

    public void ResetTime()
    {
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        alarmRinging = false;
        canInteract = false;
        timer = 0;
        counter = 0;
    }

    private IEnumerator StartDayDelay()
    {
        GameManager.GetManager().canvasController.Lock(false);
        yield return new WaitForSeconds(4);
    }

    #region Alex Event
    bool unique;
    public void AfirmativoAlex()
    {
        if (!eventAlex.activeSelf && !unique && GameManager.GetManager().dayController.GetDayNumber() != DayController.Day.two) return;
        print("tus muertos");
        unique = false;
        eventAlex.SetActive(false);
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op3_Op1", delegate
        {
            GameManager.GetManager().alexVisited = true;
            StartCoroutine(NormalWakeUp());
        });
    }

    bool negated;
    public void NegativeAlex()
    {
        if (!eventAlex.activeSelf && !unique && GameManager.GetManager().dayController.GetDayNumber() != DayController.Day.two) return;
        print("HOLA");
        unique = false;
        eventAlex.SetActive(false);
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op3_Op2", delegate
        {
            negated = true;
            GameManager.GetManager().autocontrol.RemoveAutoControl(5);
            StillSleeping();
            GameManager.GetManager().alexController.PaCasa();
            GameManager.GetManager().UnlockBasicTasks();
        });
    }
    #endregion
}
