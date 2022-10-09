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
        alarmsfx.start();
        alarmsfx.release();
        inbed.start();

        Show();
        //NO ME BORR�IS ESTE IF !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.one)
        //{
        //    if(counter>0)
        //        Show();
        //    else
        //        GameManager.GetManager().dialogueManager.StartDialogue("Alarm", delegate { Show(); });
        //}
    }
    public IEnumerator NormalWakeUp()
    {
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.one)
        {
            string name;
            if (counter == 0) name = "GetUp1";
            else name = "GetUp2";

            GameManager.GetManager().dialogueManager.StartDialogue(name);
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/AlarmOff");
        GameManager.GetManager().cameraController.StartInteractCam(2);
        CanvasAlarm.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().playerController.PlayerWakeUpPos();
        GameManager.GetManager().canvasController.Lock(true);
        alarm = false;
        ResetTime();

    }
    void Show()
    {
        CanvasAlarm.SetActive(true);
        timer = 0;
        alarmRinging = true;
    }
    public void StillSleeping()
    {
        alarmRinging = false;

        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.one)
        {
            string name = "alarm";
            if (counter == 0) name = "Alarm2";
            else name = "Alarm3";

            GameManager.GetManager().dialogueManager.StartDialogue(name, delegate
            {
                StartAlarm();
            });

            if (counter >= 2)
                StartAlarm();
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
        inbed.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        alarmRinging = false;
        timer = 0;
    }

    private IEnumerator StartDayDelay()
    {
        GameManager.GetManager().canvasController.Lock(false);
        yield return new WaitForSeconds(4);
    }
}
