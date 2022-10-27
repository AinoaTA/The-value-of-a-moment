using System.Collections;
using TMPro;
using UnityEngine;

public class Shower : GeneralActions, ITask
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

    private static FMOD.Studio.EventInstance ShowerSFX;
    private float lowAutoConfidenceLimit = 50f;
    public GameObject cortinas;
    #region Extra Actions
    public DoSomething[] moreOptions;
    [System.Serializable]
    public struct DoSomething
    {
        public string name;
        public string animationName;
        public Interactables interaction;
        public string canvasText;
        //dialogues xdxd
    }

    bool duchado;

    private Vector3 positionOnEnter;

    public Animator canvas;
    public TMP_Text[] texts;
    public GameObject showerPos;

    void StartExtraInteraction(int id)
    {
        if (GameManager.GetManager().interactableManager.currInteractable == null)
        {
            GameManager.GetManager().interactableManager.LookingAnInteractable(moreOptions[id].interaction);
            moreOptions[id].interaction.ExtraInteraction();
        }
    }

    #endregion
    IEnumerator routine;
    public override void EnterAction()
    {
        base.EnterAction();
        ShowerSFX.start();
        ShowerSFX.setParameterByName("ShowerOnOff", 0f);
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        positionOnEnter = GameManager.GetManager().playerController.GetPlayerPos();
        GameManager.GetManager().playerController.SetPlayerPos(showerPos.transform.position);
        duchado = true;
        StartCoroutine(routine = Cortinas(true));
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("D2AccHigLimp_Ducha");
            GameManager.GetManager().IncrementInteractableCount();
        }
        if (GameManager.GetManager().autocontrol.m_currentValue < lowAutoConfidenceLimit)
        {
            //StartCoroutine(ShowOtherOptions());
        }
    }

    public override void ExitAction()
    {
        if (routine != null)
            StopCoroutine(routine);
        print("??");
        CheckDoneTask();
        StartCoroutine(routine = Cortinas(false));
        InteractableBlocked = true;
        ShowerSFX.setParameterByName("ShowerOnOff", 1f);
        ShowerSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.GetManager().dayController.TaskDone();
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        GameManager.GetManager().playerController.ResetPlayerPos(positionOnEnter);
        base.ExitAction();

        string ducha;
        if (!duchado) ducha = "DuchaNo";
        else ducha = "DuchaSi";

        GameManager.GetManager().dialogueManager.SetDialogue(ducha, delegate
        {
            StartCoroutine(Delay());
        });
    }
    IEnumerator Delay()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                canvas.gameObject.SetActive(false);
                yield return new WaitForSeconds(1);
                GameManager.GetManager().dialogueManager.SetDialogue("TutorialAgenda", delegate
                {
                    GameManager.GetManager().dayController.ChangeDay(1);
                    GameManager.GetManager().blockController.UnlockAll(DayController.DayTime.MedioDia);
                });

                break;
            case DayController.Day.two:
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        ShowerSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Env/Shower");
        SetTask();
        // canvas.SetBool("Showing", true);
    }

    public void ShowerOnOff(float ShowerStat)
    {
        ShowerSFX.setParameterByName("ShowerOnOff", ShowerStat);
    }

    IEnumerator ShowOtherOptions()
    {
        yield return null;
        //for (int i = 0; i < texts.Length; i++)
        //    texts[i].text = moreOptions[i].canvasText;

        //yield return new WaitForSeconds(1f);
        //canvas.gameObject.SetActive(true);
        //canvas.SetBool("Showing", true);
    }

    public override void DoInteraction(int id)
    {
        //if (id == 0) duchado = true;
        //StartExtraInteraction(id);
    }
    public override void ResetObject()
    {
        TaskReset();
        duchado = false;
        base.ResetObject();
    }

    IEnumerator Cortinas(bool open)
    {
        float t = 0;
        Vector3 scale = cortinas.transform.localScale;
        while (t < 1)
        {
            t += Time.deltaTime;
            if (open)
                cortinas.transform.localScale = Vector3.Lerp(scale, new Vector3(2, 1, 1), t);
            else
                cortinas.transform.localScale = Vector3.Lerp(scale, new Vector3(1, 1, 1), t);
            yield return null;
        }
    }
}
