using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : Interactables, ITask
{
    #region TASK
    [Space(20)]
    [Header("TASK")]
    [SerializeField] private string nameTask_;
    [SerializeField] private Calendar.TaskType.Task task_;
    [SerializeField] private int extraAutocontrol = 5;
    [SerializeField] private Calendar.TaskType taskType_;
    private bool taskCompleted_;

    public int extraAutocontrolByCalendar { get => extraAutocontrol; }
    public bool taskCompleted { get => taskCompleted_; set => taskCompleted_ = value; }
    public string nameTask { get => nameTask_; }
    public Calendar.TaskType.Task task { get => task_; }
    public Calendar.TaskType taskAssociated { get => taskType_; set => taskType_ = value; }

    public void TaskReset()
    {
        taskCompleted = false;
    }

    public void TaskCompleted()
    {
        taskCompleted = true;
    }

    public void RewardedTask()
    {
        Debug.Log("Rewarded Task");
        GameManager.GetManager().autocontrol.AddAutoControl(extraAutocontrolByCalendar);
    }

    public void SetTask()
    {
        GameManager.GetManager().calendarController.CreateTasksInCalendar(this);
    }

    public void CheckDoneTask()
    {
        Calendar.CalendarController cal = GameManager.GetManager().calendarController;
        if (cal.CheckReward(taskAssociated))
        {
            if (cal.CheckTimeTaskDone(GameManager.GetManager().dayController.GetTimeDay(), taskAssociated.calendar.type))
            {
                TaskCompleted();
                cal.GetTaskReward(this);
            }
        }
    }

    #endregion

    public DrumRhythm[] rhythm;
    public List<DrumInstrument> instruments;
    public float delayStart = 1f;
    public float delayNextInstrument = 0.5f;
    public float delayFinish = 1f;
    public string finalPlayCameraName;

    int rhythmPosition = 0;
    bool playingDrum = false;
    DrumInstrument pointedInstrument;
    public BoxCollider col;
    public MeshRenderer mesh;
    public Material drumMat;
    private int day;

    public FMODMusic MusicGameplay;
    private void Start()
    {
        SetTask();
    }
    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                // Inicia minijuego
                MusicGameplay.Drums(1f);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                GameManager.GetManager().canvasController.Lock();
                col.enabled = false;
                playingDrum = false;
                if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
                {
                    GameManager.GetManager().dialogueManager.SetDialogue("D2AccSelfcOcio_Bateria");
                    GameManager.GetManager().IncrementInteractableCount();
                }
                StartCoroutine(StartActivity());
                break;
        }
    }

    public override void ExitInteraction()
    {
        col.enabled = true;
        CheckDoneTask();
        StopPlayingDrum();
        MusicGameplay.Drums(0f);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().dialogueManager.SetDialogue("IBateria");
        mesh.material = drumMat;
        base.ExitInteraction();
    }

    void Update()
    {
        if (playingDrum) DetectPlayingDrum();
    }

    IEnumerator StartActivity()
    {
        day = (int)GameManager.GetManager().dayController.GetDayNumber();

        yield return new WaitForSeconds(delayStart);
        rhythmPosition = 0;
        ShowNextInstrument();
    }

    void ShowNextInstrument()
    {
        if (rhythmPosition >= rhythm[day].instrumentsOrder.Length)
        {
            StartPlayerPractice();
            return;
        }

        instruments[rhythm[day].instrumentsOrder[rhythmPosition]].SetRight();
        StartCoroutine(WaitNextInstrument());
    }

    IEnumerator WaitNextInstrument()
    {
        yield return new WaitForSeconds(delayNextInstrument);
        instruments[rhythm[day].instrumentsOrder[rhythmPosition]].Restore();
        rhythmPosition++;
        ShowNextInstrument();
    }

    void StartPlayerPractice()
    {
        rhythmPosition = 0;
        playingDrum = true;

        foreach (DrumInstrument instrument in instruments)
            instrument.Enable(true);
    }

    void DetectPlayingDrum()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            RestorePointedInstrument();
            return;
        }

        LookForInstrumentPointed(hit.collider.gameObject);
        if (pointedInstrument && Input.GetMouseButtonDown(0)) PlayInstrument();
    }

    void LookForInstrumentPointed(GameObject objectPointed)
    {
        if (pointedInstrument && pointedInstrument.gameObject == objectPointed) return;

        RestorePointedInstrument();
        foreach (DrumInstrument instrument in instruments)
            if (instrument.gameObject == objectPointed)
            {
                instrument.MouseOver(true);
                pointedInstrument = instrument;
                return;
            }

        pointedInstrument = null;
    }

    void RestorePointedInstrument()
    {
        if (!pointedInstrument) return;
        pointedInstrument.MouseOver(false);
        pointedInstrument = null;
    }

    void PlayInstrument()
    {
        if (pointedInstrument != instruments[rhythm[day].instrumentsOrder[rhythmPosition]])
        {
            StopPlayingDrum();
            foreach (DrumInstrument instrument in instruments)
                instrument.Restore();
            pointedInstrument.SetWrong();
            GameManager.GetManager().dialogueManager.SetDialogue("InstrumentoFalla", canRepeat: true);
            StartCoroutine(FailPerformance());
            return;
        }

        rhythmPosition++;
        if (rhythmPosition >= rhythm[day].instrumentsOrder.Length)
        {
            StopPlayingDrum();
            GameManager.GetManager().dialogueManager.SetDialogue("InstrumentoAcierta", canRepeat: true);
            StartCoroutine(PerformedSuccessfully());
        }

        RestoreAllInstruments();
        pointedInstrument.SetRight();
    }

    IEnumerator FailPerformance()
    {
        yield return new WaitForSeconds(delayStart);

        RestoreAllInstruments();
        rhythmPosition = 0;
        ShowNextInstrument();
    }

    IEnumerator PerformedSuccessfully()
    {
        interactDone = true;
        GameManager.GetManager().dayController.TaskDone();
        yield return new WaitForSeconds(delayFinish);
        RestoreAllInstruments();
        GameManager.GetManager().cameraController.StartInteractCam(finalPlayCameraName);
    }

    void StopPlayingDrum()
    {
        if (playingDrum)
        {
            playingDrum = false;
            foreach (DrumInstrument instrument in instruments)
                instrument.Enable(false);
        }
    }

    void RestoreAllInstruments()
    {
        foreach (DrumInstrument instrument in instruments)
            instrument.Restore();
    }

    public override void ResetInteractable()
    {
        RestoreAllInstruments();
        TaskReset();
        base.ResetInteractable();
    }
}
