using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public string[] EllePhrases;
    public VoiceOff[] WakeUpVoice;
    public GameObject CanvasAlarm;

    public float Autocontrol;

    [SerializeField] private float MaxTime;
    private bool Alarm = true;
    private int count;
    [SerializeField] private float Timer;
    private bool AlarmON;
    private int controlPosponer;
    private bool temp;


    public delegate void DelegateSFX();
    public static DelegateSFX DelegateSFX;

    private void Start()
    {
        GameManager.GetManager().cameraController.StartInteractCam(1);
        //GameManager.GetManager().Alarm = this;
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
        //Debug.Log("commented input");
#if UNITY_EDITOR
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    ResetTime();
        //    StartCoroutine(NormalWakeUp());
        //}
#endif
        if (Alarm && !AlarmON)
            Timer += Time.deltaTime;
         
        if ((Timer > MaxTime) && !AlarmON)
            StartAlarm();

        //if (AlarmON && GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.Init)
        //{
        //    Debug.Log("commented input");
        //    //if (Input.GetKeyDown(KeyCode.E))
        //    //{
        //    //   
        //    //}
        //    //else if (Input.GetKeyDown(KeyCode.Q))
        //    //{
        //    //    StillSleeping();
        //    //}
        //}
    }

    private void StartDay()
    {
        if (AlarmON && GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.Init)
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
     
        if (AlarmON && GameManager.GetManager().gameStateController.CurrentStateGame == GameStateController.StateGame.Init)
        {
            StillSleeping();
        }
    }

    private void StartAlarm()
    {
        DelegateSFX?.Invoke();
        GameManager.GetManager().SoundController.QuitAllMusic();

        CanvasAlarm.SetActive(true);
        Timer = 0;
        AlarmON = true;
        //sonid
    }
    public IEnumerator NormalWakeUp()
    {

        // GameManager.GetManager().PlayerController.SetInteractable("WakeUp");
        GameManager.GetManager().cameraController.StartInteractCam(2);
        GameManager.GetManager().SoundController.SetMusic();
        CanvasAlarm.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().PlayerController.PlayerWakeUpPos();
        GameManager.GetManager().CanvasManager.Pointer.SetActive(true);
        Alarm = false;
        ResetTime();
        yield return new WaitForSeconds(3f);
        GameManager.GetManager().StartThirdPersonCamera();

        if (!temp)
        {
            //if (controlPosponer == 0)
            //    StartCoroutine(WakeUpDialogue());
            //else
            //    StartCoroutine(SecondWakeUpDialogue());
        }
        yield return null;
    }



    public void StillSleeping()
    {
        //if (controlPosponer == 0)
        //    StartCoroutine(PosponerDialogue());
        //else
        //    StartCoroutine(SecondPosponerDialogue());

        controlPosponer++;

        AlarmON = false;

        if (count >= EllePhrases.Length)
            count = 0;

        Alarm = true;
        ResetTime();
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().gameStateController.ChangeGameState(0);
        GameManager.GetManager().Autocontrol.RemoveAutoControl(Autocontrol);
        //GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[count], null);
        count++;
    }

    public bool GetIsActive()
    {
        return Alarm;
    }

    public void SetAlarmActive()
    {
        Alarm = true;
    }

    public void ResetTime()
    {
        AlarmON = false;
        GameManager.GetManager().SoundController.StopSound();
        Timer = 0;
        
    }

    private IEnumerator StartDayDelay()
    {
        yield return new WaitForSeconds(4);
        int counter = 0;

        //GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[counter]);

        yield return new WaitForSeconds(3);

        //GameManager.GetManager().Dialogue.SetDialogue(WakeUpVoice[counter]);
        counter++;
        //yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());

        //GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[counter]);
        yield return new WaitForSeconds(3f);
        //GameManager.GetManager().Dialogue.StopDialogue();
    }

    //private IEnumerator WakeUpDialogue()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.SetDialogue(WakeUpVoice[1]);
    //    yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //    GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[2]);
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.StopDialogue();
    //    temp = true;

    //    //temp hint
    //    GameManager.GetManager().Window.StartVoiceOffDialogueWindow();
    //}
    //private IEnumerator SecondWakeUpDialogue()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.SetDialogue(WakeUpVoice[1]);
    //    yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //    GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[4]);
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.StopDialogue();
    //    //temp hint
    //    GameManager.GetManager().Window.StartVoiceOffDialogueWindow();
    //    temp = true;
    //}

    //private IEnumerator PosponerDialogue()
    //{
    //    yield return new WaitForSeconds(1);
    //    GameManager.GetManager().Dialogue.SetDialogue(WakeUpVoice[2]);
    //    yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //    GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[3]);
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.StopDialogue();
    //}

    //private IEnumerator SecondPosponerDialogue()
    //{
    //    yield return new WaitForSeconds(1);
    //    GameManager.GetManager().Dialogue.SetDialogue(WakeUpVoice[4]);
    //    yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
    //    GameManager.GetManager().Dialogue.SetDialogue(EllePhrases[5]);
    //    yield return new WaitForSeconds(1.5f);
    //    GameManager.GetManager().Dialogue.StopDialogue();
    //}
}
