using System.Collections.Generic;
using UnityEngine;

namespace Calendar
{
    public class CalendarController : MonoBehaviour
    {
        public Transform TaskMovement;
        public Dictionary<TaskType, SpaceCalendar> calendarInformation;
        [SerializeField] private List<SpaceCalendar> allTimeTable = new List<SpaceCalendar>();
        //public List<TaskType> allTask = new List<TaskType>();
        [SerializeField] private GameObject calendar;
        [SerializeField] private CanvasGroup modifiedBlock;
        [SerializeField] private MobileCalendar mobileCalendar;
        [SerializeField] private bool modified;

        private void Start()
        {
            GameManager.GetManager().calendarController = this;
            calendarInformation = new Dictionary<TaskType, SpaceCalendar>();
        }
        public void BackCalendar()
        {
            calendar.SetActive(false);
            modifiedBlock.gameObject.SetActive(false);
            GameManager.GetManager().computer.ComputerON();
        }
        public void SaveCalendar()
        {
            if (!modified)
            {
                modified = true;
                RevisionCalendar();
                for (int a = 0; a < allTimeTable.Count; a++)
                {
                    for (int i = 0; i < allTimeTable[a].taskSave.Count; i++)
                    {
                        calendarInformation.Add(allTimeTable[a].taskSave[i], allTimeTable[a]);
                        //allTask.Add(allTimeTable[a].taskSave[i]);
                    }
                }
            }
        }
        public void RevisionCalendar()
        {
            modifiedBlock.gameObject.SetActive(true);
        }

        public void ShowCalendar()
        {
            calendar.SetActive(true);
            if (!modified)
            {
                modifiedBlock.gameObject.SetActive(false);
            }
            else
                RevisionCalendar();
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