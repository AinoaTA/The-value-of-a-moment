using UnityEngine;

namespace Calendar
{
    public class Tasks : MonoBehaviour
    {
        [Header("Calendar")]
        [SerializeField] private float extraAutocontrolByCalendar;
        public string nameTask;
        public TaskType.Task task;
        public TaskType taskAssociated;
        public bool taskCompleted;

        public void TaskReset()
        {
            taskCompleted = false;
        }

        public void TaskCompleted()
        {
            taskCompleted = true;
            GameManager.GetManager().calendarController.CheckTask(this);
        }

        public void RewardedTask()
        {
            Debug.Log("Rewarded Task");
            GameManager.GetManager().autocontrol.AddAutoControl(extraAutocontrolByCalendar);
        }
    }
}
