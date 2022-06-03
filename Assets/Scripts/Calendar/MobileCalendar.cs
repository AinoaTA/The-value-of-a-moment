using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MobileCalendar : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;
    public GameObject selected, noselected;

    public void OpenCalendar()
    {
        if (GameManager.GetManager().calendarController.calendarInformation.Count == 0)
        {
            noselected.SetActive(true);
            return;
        }else
            selected.SetActive(true);

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
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
