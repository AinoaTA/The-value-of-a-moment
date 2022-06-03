using System.Collections.Generic;
using UnityEngine;

public class CalendarController : MonoBehaviour
{
    public Transform TaskMovement;
    public List<SpaceCalendar> allTimeTable = new List<SpaceCalendar>();

    public Dictionary<TaskType, SpaceCalendar> calendarInformation = new Dictionary<TaskType, SpaceCalendar>();
    public CanvasGroup canvasGroup;
    [SerializeField]private bool modified;
    private void Start()
    {
        GameManager.GetManager().calendarController = this;
    }
    public void BackCalendar()
    {
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
                    print(calendarInformation.Keys);
                }
            }
        }
    }
    public void RevisionCalendar()
    {
        canvasGroup.alpha = 1;
    }

    public void ShowCalendar()
    {
        if (!modified)
        {
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
}
