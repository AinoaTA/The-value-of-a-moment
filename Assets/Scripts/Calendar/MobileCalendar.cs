using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MobileCalendar : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;
    public GameObject selected, noselected;
    public TMP_Text textTime;

    private void OnEnable()
    {
        TaskType.taskDelegate += TaskDone;
    }

    private void OnDisable()
    {
        TaskType.taskDelegate -= TaskDone;
    }
    public void OpenCalendar()
    {
        if (GameManager.GetManager().calendarController.calendarInformation.Count == 0)
        {
            noselected.SetActive(true);
            return;
        }
        else
        {
            textTime.text = "Hora del día: "+ GameManager.GetManager().dayNightCycle.m_DayState.ToString();
            selected.SetActive(true);
        }

        if (content.childCount == 0)
        {
            
            foreach (KeyValuePair<TaskType, SpaceCalendar> item in GameManager.GetManager().calendarController.calendarInformation)
            {
                GameObject taskView = Instantiate(prefab, transform.position, Quaternion.identity, content);
                
                taskView.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Value.type.ToString();
                taskView.transform.GetChild(1).GetComponent<TMP_Text>().text = item.Key.nameTask.ToString();
            }
        }
    }

    public void CloseCalendar()
    {
        selected.SetActive(false);
        noselected.SetActive(false);
        //gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void ResetCalendar()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    
    }

    public void TaskDone(TaskType tipe)
    { int index=0;
        foreach (TaskType item in GameManager.GetManager().calendarController.calendarInformation.Keys)
        { index++;
            if (item == tipe)
            {
                content.GetChild(index).GetComponent<Image>().color = Color.green;
                return;
            }
        }
    
    }
}
