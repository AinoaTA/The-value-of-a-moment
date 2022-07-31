using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    public int extraAutocontrolByCalendar { get; }
    public bool taskCompleted { get; set; }
    public string nameTask { get; }
    public Calendar.TaskType.Task task { get; }
    public Calendar.TaskType taskAssociated { get; set; }

    public void TaskReset();

    public void TaskCompleted();

    public void RewardedTask();

    public void SetTask();

    public void CheckDoneTask();
}
