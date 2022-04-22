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
    public delegate void DelegateSFX();
    public static DelegateSFX m_DelegateSFX;

    private void Awake()
    {
        GameManager.GetManager().Alarm = this;
        CanvasAlarm.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(StartDay());
        GameManager.GetManager().PlayerController.SetInteractable("Alarm");
    }

    private void Update()
    {
        if (m_Alarm && !m_AlarmON)
            m_Timer += Time.deltaTime;

        if ((m_Timer > m_MaxTime) && !m_AlarmON)
            StartAlarm();


        if (m_AlarmON && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.Init)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ResetTime();
                NormalWakeUp();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                StillSleeping();
            }
        }
    }

    private void StartAlarm()
    {
        m_DelegateSFX?.Invoke();
        GameManager.GetManager().SoundController.QuitMusic();
        CanvasAlarm.SetActive(true);
        m_Timer = 0;
        m_AlarmON = true;
        //sonid
    }
    public void NormalWakeUp()
    {
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.GamePlay;
        GameManager.GetManager().SoundController.SetMusic();
        CanvasAlarm.SetActive(false);

        GameManager.GetManager().PlayerController.PlayerWakeUpPos();
        GameManager.GetManager().PlayerController.ExitInteractable();

        m_Alarm = false;
        ResetTime();
        GameManager.GetManager().CanvasManager.Pointer.SetActive(true);

        if (controlPosponer == 0)
            StartCoroutine(WakeUpDialogue());
        else
            StartCoroutine(SecondWakeUpDialogue());
        //GameManager.GetManager().Dialogue.SetTimer();

        ///GameManager.GetManager().GetCanvasManager().FadeInSolo();
        //animacion player se levanta

    }


    public void StillSleeping()
    {
        if (controlPosponer == 0)
            StartCoroutine(PosponerDialogue());
        else
            StartCoroutine(SecondPosponerDialogue());

        controlPosponer++;

        m_AlarmON = false;

        if (m_count >= m_EllePhrases.Length)
            m_count = 0;

        m_Alarm = true;
        ResetTime();
        GameManager.GetManager().m_CurrentStateGame = GameManager.StateGame.Init;
        CanvasAlarm.SetActive(false);
        GameManager.GetManager().Autocontrol.RemoveAutoControl(m_Autocontrol);
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
        GameManager.GetManager().SoundController.StopSound();
        m_Timer = 0;
        m_AlarmON = false;
    }

    private IEnumerator StartDay()
    {
        yield return new WaitForSeconds(4);
        int counter = 0;

        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[counter]);

        yield return new WaitForSeconds(3);

        GameManager.GetManager().Dialogue.SetDialogue(m_WakeUpVoice[counter]);
        counter++;
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());

        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[counter]);
        yield return new WaitForSeconds(3f);
        GameManager.GetManager().Dialogue.StopDialogue();
    }

    private IEnumerator WakeUpDialogue()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.SetDialogue(m_WakeUpVoice[1]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[2]);
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.StopDialogue();

        //temp hint
        GameManager.GetManager().Window.StartVoiceOffDialogueWindow();
    }
    private IEnumerator SecondWakeUpDialogue()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.SetDialogue(m_WakeUpVoice[1]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[4]);
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.StopDialogue();

        //temp hint
        GameManager.GetManager().Window.StartVoiceOffDialogueWindow();
    }

    private IEnumerator PosponerDialogue()
    {
        yield return new WaitForSeconds(1);
        GameManager.GetManager().Dialogue.SetDialogue(m_WakeUpVoice[2]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[3]);
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.StopDialogue();
    }

    private IEnumerator SecondPosponerDialogue()
    {
        yield return new WaitForSeconds(1);
        GameManager.GetManager().Dialogue.SetDialogue(m_WakeUpVoice[4]);
        yield return new WaitWhile(() => GameManager.GetManager().Dialogue.CheckDialogueIsPlaying());
        GameManager.GetManager().Dialogue.SetDialogue(m_EllePhrases[5]);
        yield return new WaitForSeconds(1.5f);
        GameManager.GetManager().Dialogue.StopDialogue();
    }
}
