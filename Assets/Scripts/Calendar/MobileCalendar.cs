using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calendar
{
    public class MobileCalendar : MonoBehaviour
    {
        public Transform content;
        public GameObject prefab;
        public GameObject selected, noselected;
        public Image bgDay;
        public Sprite[] timeDaySprites;
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
            bgDay.sprite = timeDaySprites[(int)GameManager.GetManager().dayController.GetTimeDay()];
            if (GameManager.GetManager().calendarController.calendarInformation.Count == 0)
            {
                noselected.SetActive(true);
                return;
            }
            else
                selected.SetActive(true);

            if (content.childCount == 0)
            {
                foreach (KeyValuePair<TaskType, SpaceCalendar> item in GameManager.GetManager().calendarController.calendarInformation)
                    if (item.Value.timeDate == GameManager.GetManager().dayController.GetTimeDay())
                        HandleTaskView(item);
            }
        }

        private void HandleTaskView(KeyValuePair<TaskType, SpaceCalendar> item)
        {
            GameObject taskView = Instantiate(prefab, transform.position, Quaternion.identity, content);
            taskView.GetComponent<Image>().sprite = item.Key.sprite.sprite;
            taskView.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Value.timeName[(int)item.Value.type];
            taskView.transform.GetChild(1).GetComponent<TMP_Text>().text = item.Key.nameTask.ToString();
        }

        public void CloseCalendar()
        {
            selected.SetActive(false);
            noselected.SetActive(false);
            ResetCalendar();
        }
        public void ResetCalendar()
        {
            for (int i = 0; i < content.childCount; i++)
                Destroy(content.GetChild(i).gameObject);
        }

        public void TaskDone(TaskType type)
        {
            int index = 0;
            foreach (TaskType item in GameManager.GetManager().calendarController.calendarInformation.Keys)
            {
                index++;
                if (item == type)
                {
                    content.GetChild(index).GetComponent<Image>().color = Color.green;
                    return;
                }
            }
        }
    }
}
