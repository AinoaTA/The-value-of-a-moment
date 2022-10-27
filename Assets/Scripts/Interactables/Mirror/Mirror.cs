using System.Collections;
using UnityEngine;

public class Mirror : Interactables,ITask
{
    //private int m_Counter = 0;

    public string[] bad1, lessbad, normal, good;
    private int counterbad1, counterless, counternormal, countergood;

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
    private void Start()
    {
        SetTask();
    }
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                if (!interactDone)
                {
                    interactDone = true;
                    GameManager.GetManager().gameStateController.ChangeGameState(2);
                    GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);

                    StartCoroutine(LookUp());
                }
                break;
        }
    }

    private IEnumerator LookUp()
    {
        yield return new WaitForSeconds(1);
        if (GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.3f)
        {
            //  GameManager.GetManager().Dialogue.SetDialogue(bad1[counterbad1]);
            counterbad1++;
            if (counterbad1 >= bad1.Length)
                counterbad1 = 0;

            GameManager.GetManager().dialogueManager.SetDialogue("IEspejo", canRepeat: true);

            GameManager.GetManager().playerController.SadMoment();
            GameManager.GetManager().autocontrol.RemoveAutoControl(15);

        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.3f && GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.5f)
        {
            //GameManager.GetManager().Dialogue.SetDialogue(lessbad[counterless]);
            counterless++;
            if (counterless >= lessbad.Length)
                counterless = 0;
            GameManager.GetManager().playerController.SadMoment();
            GameManager.GetManager().autocontrol.RemoveAutoControl(15);
        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.5f && GameManager.GetManager().autocontrol.GetAutcontrolValue() <= 0.8f)
        {
            // GameManager.GetManager().Dialogue.SetDialogue(normal[counternormal]);
            counternormal++;
            if (counternormal >= normal.Length)
                counternormal = 0;
            GameManager.GetManager().playerController.HappyMoment();
            GameManager.GetManager().autocontrol.AddAutoControl(15);
        }
        else if (GameManager.GetManager().autocontrol.GetAutcontrolValue() > 0.8f)
        {
            // GameManager.GetManager().Dialogue.SetDialogue(good[countergood]);
            countergood++;
            if (countergood >= good.Length)
                countergood = 0;

            GameManager.GetManager().playerController.HappyMoment();
            GameManager.GetManager().autocontrol.AddAutoControl(2);
        }

        yield return new WaitForSeconds(2);
        //   GameManager.GetManager().Dialogue.StopDialogue();
        CheckDoneTask();
        GameManager.GetManager().StartThirdPersonCamera();
    }

    public override void ResetInteractable()
    {
        interactDone = false;
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }
}
