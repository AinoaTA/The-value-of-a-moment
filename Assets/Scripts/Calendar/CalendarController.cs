using System.Collections.Generic;
using UnityEngine;

public class CalendarController : MonoBehaviour
{
    public Transform TaskMovement;
    public List<SpaceCalendar> allTimeTable = new List<SpaceCalendar>();
    //public List<TaskType> allTask = new List<TaskType>();
    public Dictionary<TaskType, SpaceCalendar> calendarInformation;
    public CanvasGroup canvasGroup;
    public CanvasGroup modifiedBlock;
    public MobileCalendar mobileCalendar;
    [SerializeField]private bool modified;
    private void Start()
    {
        GameManager.GetManager().calendarController = this;
        calendarInformation = new Dictionary<TaskType, SpaceCalendar>();
    }
    public void BackCalendar()
    {
        modifiedBlock.gameObject.SetActive(false);
        canvasGroup.gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    public void SaveCalendar()
    {
        if (!modified)
        {
            modified = true;

            for (int a = 0; a <allTimeTable.Count ; a++)
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
        canvasGroup.alpha = 1;
    }

    public void ShowCalendar()
    {
        canvasGroup.gameObject.SetActive(true);
        if (!modified)
        {
            modifiedBlock.gameObject.SetActive(false);
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
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
