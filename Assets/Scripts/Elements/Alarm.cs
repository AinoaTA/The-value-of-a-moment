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
    private int controlPosponer;
    private bool temp;


    public delegate void DelegateSFX();
    public static DelegateSFX m_DelegateSFX;

    private void Start()
    {
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
         
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartDay();
        }
#endif
        if (m_Alarm && !m_AlarmON)
            m_Timer += Time.deltaTime;
         
        if ((m_Timer > m_MaxTime) && !m_AlarmON)
            StartAlarm();

    }

    private void StartDay()
    {
        if (m_AlarmON && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.Init)
        {
            ResetTime();
            StartCoroutine(NormalWakeUp());
        }
    }

    private void BackDay()
    {
        if (m_AlarmON && GameManager.GetManager().gameStateController.m_CurrentStateGame == GameStateController.StateGame.Init)
        {
            StillSleeping();
        }
    }

    private void StartAlarm()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>(); // <-- Added by Aryadna to test
        if (null!=dialogueManager) dialogueManager.StartDialogue("Alarm"); // <-- Added by Aryadna to test
        m_DelegateSFX?.Invoke();
        GameManager.GetManager().soundController.QuitAllMusic();

        CanvasAlarm.SetActive(true);
        m_Timer = 0;
        m_AlarmON = true;
        //sonid
    }
    public IEnumerator NormalWakeUp()
    {

        // GameManager.GetManager().PlayerController.SetInteractable("WakeUp");
        GameManager.GetManager().cameraController.StartInteractCam(2);
        GameManager.GetManager().soundController.SetMusic();
        CanvasAlarm.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameManager.GetManager().playerController.PlayerWakeUpPos();
        GameManager.GetManager().canvasController.Pointer.SetActive(true);
        m_Alarm = false;
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

        m_AlarmON = false;

        if (m_count >= m_EllePhrases.Length)
            m_count = 0;

        m_Alarm = true;
        ResetTime();
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().gameStateController.ChangeGameState(0);
        GameManager.GetManager().autocontrol.RemoveAutoControl(m_Autocontrol);
        //GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[m_count], null);
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
        m_AlarmON = false;
        GameManager.GetManager().soundController.StopSound();
        m_Timer = 0;
    }

    private IEnumerator StartDayDelay()
    {
        GameManager.GetManager().canvasController.Lock(false);
        yield return new WaitForSeconds(4);
        
    }
}
