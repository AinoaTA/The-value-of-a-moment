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
    [SerializeField] private bool talking;
    public FMODMusic MusicGameplay;
    public GameObject eventAlex;
    private static FMOD.Studio.EventInstance alarmsfx;

    private void Start()
    {
        GameManager.GetManager().alarm = this;
        alarmsfx = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Alarm");

        GameManager.GetManager().cameraController.StartInteractCam(1);
        CanvasAlarm.SetActive(false);
        StartCoroutine(StartDayDelay());

        if (GameManager.GetManager().dayController.currentDay == DayController.Day.two)
        {
            GameManager.GetManager().playerInputs._FirstInteraction += AfirmativoAlex;
            GameManager.GetManager().playerInputs._SecondInteraction += NegativeAlex;
        }
        GameManager.GetManager().playerInputs._FirstInteraction += StartDay;
        GameManager.GetManager().playerInputs._SecondInteraction += BackDay;
    }

    private void OnDisable()
    {
        if (GameManager.GetManager().dayController.currentDay == DayController.Day.two)
        {
            GameManager.GetManager().playerInputs._FirstInteraction += AfirmativoAlex;
            GameManager.GetManager().playerInputs._SecondInteraction += NegativeAlex;
        }

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
        if (alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
        if (alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
        {
            StillSleeping();
        }
    }
    private void StartAlarm()
    {
        talking = false;
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                AlarmAndMood();
                if (counter > 0)
                    Show();
                else
                    GameManager.GetManager().dialogueManager.SetDialogue("Alarm", delegate { Show(); });
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
                },forceInvoke:true);
                break;
            case DayController.Day.three:

                GameManager.GetManager().dialogueManager.SetDialogue("D3Start", delegate
                {
                    AlarmAndMood();
                    Show();
                });
                break;
            case DayController.Day.fourth:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void AlarmAndMood()
    {
        talking = false;
        alarmsfx.start();
        MusicGameplay.Mood(0f);
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
                if (GameManager.GetManager().alexVisited) yield break;
                GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1", delegate
                {
                    GameManager.GetManager().alexVisited = true;
                    StartCoroutine(Delay2());
                });
                break;

            case DayController.Day.three:
                string names;
                if (counter == 0) names = "D3Start_Op1";
                else names = "D3Start_Op2a";
                GameManager.GetManager().dialogueManager.SetDialogue(names, delegate
                {
                    StartCoroutine(Delay());
                });

                break;

            case DayController.Day.fourth:
                break;
            default:
                throw new ArgumentOutOfRangeException();
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

    IEnumerator Delay2()
    {
        yield return new WaitWhile(() => GameManager.GetManager().dialogueManager.waitDialogue);
        GameManager.GetManager().dialogueManager.SetDialogue("Ventana", delegate
        {
            // TODO: unlock de los interactables
            GameManager.GetManager().blockController.Unlock("Ventanas");
            GameManager.GetManager().blockController.Unlock("Michi");
            GameManager.GetManager().blockController.Unlock("Ducha");
        });
        StartAlarm();
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
                    if (name == "D2Alarm_Op3" && !negated)
                    {
                        eventAlex.gameObject.SetActive(true);
                        unique = true;
                    }
                    else
                    {
                        StartAlarm();
                    }
                },forceInvoke:true);

                break;
            case DayController.Day.three:
                name = "D3Start_Op2";
                if (counter == 0) name = "D3Start_Op2b";
                else name = "Alarm3";

                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    if (name != "Alarm3")
                        StartAlarm();
                    else
                        print("BAD EEEEEEEEEEEEEND");

                });
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

    #region Alex Event
    bool unique;
    public void AfirmativoAlex()
    {
        if (!eventAlex.activeSelf && !unique) return;
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
        if (!eventAlex.activeSelf && !unique) return;
        unique = false;
        eventAlex.SetActive(false);
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op3_Op2", delegate
        {
            negated = true;
            GameManager.GetManager().autocontrol.RemoveAutoControl(5);
            StillSleeping();
        });
    }
    #endregion
}
