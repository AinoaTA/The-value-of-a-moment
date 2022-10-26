using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public GameObject CanvasAlarm;

    [SerializeField] private float maxTime;
    [SerializeField] private float timer;

    [SerializeField] private bool alarmIsActive = true;
    [SerializeField] private bool alarmRinging = false;
    [SerializeField] private int counterNarrations;

    public FMODMusic MusicGameplay;
    public GameObject eventAlex;
    private static FMOD.Studio.EventInstance alarmsfx;

    private void Awake()
    {
        GameManager.GetManager().alarm = this;
    }
    private void Start()
    {
        GameManager.GetManager().cameraController.StartInteractCam(1);
        alarmsfx = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Alarm");

        counterNarrations = 0;
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().canvasController.Lock(false, false);
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
        if (!alarmRinging && alarmIsActive)
        {
            timer += Time.deltaTime;

            if (timer >= maxTime)
                StartAlarm();
        }
    }

    private void StartDay()
    {
        if (alarmIsActive && alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
            StartCoroutine(NormalWakeUp());
        else if (eventAlex.activeSelf)
            AfirmativoAlex();
    }

    private void BackDay()
    {
        if (alarmIsActive && alarmRinging && GameManager.GetManager().gameStateController.CheckGameState(0) && !eventAlex.activeSelf)
            StillSleeping();
        else if (eventAlex.activeSelf)
            NegativeAlex();
    }
    bool once;
    private void StartAlarm()
    {
        print("StartAlarm");
        alarmRinging = true;
        MusicGameplay.Mood(0f);
        

        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            //D�A UNO ENTERATE ---------------
            case DayController.Day.one:
                alarmsfx.start();
                print("day one");
                if (counterNarrations > 0) Show();
                else GameManager.GetManager().dialogueManager.SetDialogue("Alarm", delegate { Show(); });

                break;

            //D�A DOS ENTERATE ---------------
            case DayController.Day.two:
                print("day two");

                if (counterNarrations > 0)
                {
                    alarmsfx.start();
                    Show();
                }
                else
                    GameManager.GetManager().dialogueManager.SetDialogue("D2Start", delegate 
                    {
                        alarmsfx.start();
                        Show(); 
                    });
                break;
        }
    }


    private void AlarmAndMood()
    {
        //alarmsfx.start();
        //talking = false;
        //canInteract = true;
        //print("start sound");
    }

    public IEnumerator NormalWakeUp()
    {
        CanvasAlarm.SetActive(false);
        alarmIsActive = false;
        print("wake up");

        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            //D�A UNO ----------------------
            case DayController.Day.one:

                string name = counterNarrations == 0 ? "GetUp1" : "GetUp2";
                GameManager.GetManager().dialogueManager.SetDialogue(name, delegate
                {
                    StartCoroutine(Delay());
                });

                break;
            //D�A DOS ----------------------
            case DayController.Day.two:

                GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op1", delegate
                {
                    GameManager.GetManager().alexVisited = true;
                    GameManager.GetManager().counterAlex = true;

                    StartCoroutine(Delay2());
                });


                break;
        }
        yield return null;
        counterNarrations = 0;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/AlarmOff");
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MusicGameplay.Mood(1f);
        GameManager.GetManager().cameraController.StartInteractCam(2);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().playerController.PlayerWakeUpPos();
        GameManager.GetManager().canvasController.Lock(true);
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
        print("show");
        CanvasAlarm.SetActive(true);
        timer = 0;
    }
    public void StillSleeping()
    {
        alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        CanvasAlarm.SetActive(false);
        alarmIsActive = false;

        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            //D�A UNO ----------------------
            case DayController.Day.one:

                string name = "alarm";
                name = counterNarrations == 0 ? "Alarm2" : "Alarm3";

                if (counterNarrations >= 2)
                    ResetTime();
                else
                    GameManager.GetManager().dialogueManager.SetDialogue(name, delegate { ResetTime(); });
                break;

            //D�A DOS ----------------------
            case DayController.Day.two:

                if (counterNarrations == 0) name = "D2Alarm_Op2";
                else if (counterNarrations == 1) name = "D2Alarm_Op3";
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
                        ResetTime();
                    }
                });

                if (counterNarrations >= 2)
                    ResetTime();
                break;
        }

        counterNarrations++;
        GameManager.GetManager().autocontrol.RemoveAutoControl(5);
    }

    public bool GetIsActive()
    {
        return alarmIsActive;
    }

    public void SetAlarmActive()
    {
        ResetTime();
    }

    void ResetTime()
    {
        print("reset");
        timer = 0;
        //alarmsfx.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        alarmRinging = false;
        alarmIsActive = true;
    }

    #region Alex Event
    bool unique;
    public void AfirmativoAlex()
    {
        if (!eventAlex.activeSelf && !unique && GameManager.GetManager().dayController.GetDayNumber() != DayController.Day.two) return;
        print("tus muertos si quiero alex");
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
        print("HOLA alex no quiero");
        unique = false;
        eventAlex.SetActive(false);
        GameManager.GetManager().dialogueManager.SetDialogue("D2Alarm_Op3_Op2", delegate
        {
            ResetTime();
            negated = true;
            GameManager.GetManager().autocontrol.RemoveAutoControl(5);
            GameManager.GetManager().alexController.PaCasa();
            GameManager.GetManager().UnlockBasicTasks();
        });
    }
    #endregion
}
