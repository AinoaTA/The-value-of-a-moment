using System.Collections.Generic;
using UnityEngine;

namespace Calendar
{
    public class CalendarController : MonoBehaviour
    {
        [SerializeField] private Tasks[] getAllTasks;
        //[SerializeField] private List<TaskType> interactablesTask = new List<TaskType>();
        [SerializeField] private List<SpaceCalendar> allTimeTable = new List<SpaceCalendar>();

        public GameObject prefabTask;
        public Transform taskMovement, contentTask;
        public Dictionary<TaskType, SpaceCalendar> calendarInformation;
        public List<TaskType> allTask = new List<TaskType>();

        [SerializeField] private GameObject calendar, warning;
        [SerializeField] private MobileCalendar mobileCalendar;
        [HideInInspector] public bool modified;

        private void Awake()
        {
            calendarInformation = new Dictionary<TaskType, SpaceCalendar>();

            getAllTasks = FindObjectsOfType<Tasks>();

            CreateTasksInCalendar();
        }
        private void CreateTasksInCalendar()
        {
            for (int i = 0; i < getAllTasks.Length; i++)
            {
                TaskType _task = Instantiate(prefabTask, contentTask.position, Quaternion.identity, contentTask).GetComponent<TaskType>();
                _task.task = getAllTasks[i].task;
                _task.nameTask = getAllTasks[i].nameTask;
            }
        }
        private void Start()
        {
            GameManager.GetManager().calendarController = this;
        }
        public void BackCalendar()
        {
            if (warning.activeSelf)
                return;
            calendar.SetActive(false);
            GameManager.GetManager().computer.ComputerON();
        }
        public void SaveCalendar()
        {
            if (!modified)
            {
                modified = true;
                warning.SetActive(false);
                for (int a = 0; a < allTimeTable.Count; a++)
                {
                    for (int i = 0; i < allTimeTable[a].taskSave.Count; i++)
                        calendarInformation.Add(allTimeTable[a].taskSave[i], allTimeTable[a]);
                }
            }
        }

        public void ShowCalendar()
        {
            calendar.SetActive(true);
        }

        public void ShowWarning(bool v)
        {
            if (!modified)
                warning.SetActive(v);
        }

        public bool CheckTimeTaskDone(DayNightCycle.DayState type, SpaceCalendar.SpaceType time)
        {
            return (int)type == (int)time;
        }

        public void GlobalReset()
        {
            mobileCalendar.ResetCalendar();
            for (int i = 0; i < allTimeTable.Count; i++)
            {
                for (int n = 0; n < allTimeTable[i].taskSave.Count; n++)
                {
                    allTimeTable[i].taskSave[n].ResetTask();
                }
                allTimeTable[i].taskSave.Clear();
            }

            calendarInformation.Clear();

            for (int i = 0; i < allTimeTable.Count; i++)
            {
                allTimeTable[i].taskSave.Clear();
            }
            modified = false;
        }
    }
}