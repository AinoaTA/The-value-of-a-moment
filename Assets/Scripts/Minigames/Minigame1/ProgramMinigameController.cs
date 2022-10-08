using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProgramMinigameController : MonoBehaviour, ITask
{
    public List<SolutionPiece> m_AllSolutions = new List<SolutionPiece>();
    public List<PieceMG> m_AllPieces = new List<PieceMG>();
    private bool solved = false;
    private bool checking;
    private bool m_AllCorrected;
    //private bool gameInitialized;
    public float m_Autocontrol = 5;

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
        //gameInitialized = false;
        solved = true;
        GameManager.GetManager().dayController.TaskDone();
        CheckDoneTask();
        GameManager.GetManager().autocontrol.AddAutoControl(m_Autocontrol);


        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().computer.ComputerON();
    }

    public void QuitMiniGame()
    {
        solved = false;
        //gameInitialized = false;
        GameManager.GetManager().computer.ComputerON();
    }


    public void CheckSolutions()
    {
        if (solved || checking)
            return;

        checking = true;
        for (int i = 0; i < m_AllSolutions.Count; i++)
        {
            if (!m_AllSolutions[i].m_Correct)
                m_AllCorrected = false;
            else
                m_AllCorrected = true;
        }
        checking = false;

        if (m_AllCorrected)
            StartCoroutine(GameFinished());
    }

    public void ResetAllGame()
    {
        for (int i = 0; i < m_AllPieces.Count; i++)
        {
            m_AllPieces[i].ResetPiece();
        }
        solved = false;
        //gameInitialized = false;
    }
    public bool GetSolved() { return solved; }


}
