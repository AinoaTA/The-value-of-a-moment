using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProgramMinigameController : MonoBehaviour, ITask
{
    public List<PieceMG> allPieces = new List<PieceMG>();
    private bool checking;
    private bool allCorrect;
    public float m_Autocontrol = 5;
    public SolutionPiece currSolution;

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
        GameManager.GetManager().programMinigame = this;
        SetTask();
    }
    private IEnumerator GameFinished()
    {
        GameManager.GetManager().dayController.TaskDone();
        CheckDoneTask();
        GameManager.GetManager().autocontrol.AddAutoControl(m_Autocontrol);

        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().computer.ComputerON();
    }

    public void QuitMiniGame()
    {
        allCorrect = false;
        GameManager.GetManager().computer.ComputerON(); //back to menu pc screen
    }


    public void CheckSolutions()
    {
        if (allCorrect || checking)
            return;

        checking = true;
        for (int i = 0; i < allPieces.Count; i++)
        {
            if (!allPieces[i].correctWhole)
                return;

            allCorrect = true;
        }
        checking = false;

        if (allCorrect) StartCoroutine(GameFinished());
    }

    public void ResetAllGame()
    {
        for (int i = 0; i < allPieces.Count; i++)
        {
            allPieces[i].ResetPiece();
        }
        //gameInitialized = false;
    }
    public bool GetSolved() { return allCorrect; }

    public void SetCurrSolution(SolutionPiece  e) { currSolution = e; }
}
