using UnityEngine;
using System.Collections.Generic;
using TMPro;
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
        private DayNightCycle.DayState hora;
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
            bgDay.sprite = timeDaySprites[(int)GameManager.GetManager().dayNightCycle.m_DayState];
            if (GameManager.GetManager().calendarController.calendarInformation.Count == 0)
            {
                noselected.SetActive(true);
                return;
            }
            else
            {
                selected.SetActive(true);
            }
            //refactor jajaaj adri no me mate
            if (content.childCount == 0)
            {
                foreach (KeyValuePair<TaskType, SpaceCalendar> item in GameManager.GetManager().calendarController.calendarInformation)
                {
                    if (item.Value.type == SpaceCalendar.SpaceType.Manana && GameManager.GetManager().dayNightCycle.m_DayState == DayNightCycle.DayState.Manana)
                    {
                        hora = DayNightCycle.DayState.Manana;
                    }
                    else if (item.Value.type == SpaceCalendar.SpaceType.Tarde && GameManager.GetManager().dayNightCycle.m_DayState == DayNightCycle.DayState.Tarde)
                    {
                        hora = DayNightCycle.DayState.Tarde;
                    }
                    else if (item.Value.type == SpaceCalendar.SpaceType.MedioDia && GameManager.GetManager().dayNightCycle.m_DayState == DayNightCycle.DayState.MedioDia)
                    {
                        hora = DayNightCycle.DayState.MedioDia;
                    }
                    else if (item.Value.type == SpaceCalendar.SpaceType.Noche && GameManager.GetManager().dayNightCycle.m_DayState == DayNightCycle.DayState.Noche)
                    {
                        hora = DayNightCycle.DayState.Noche;
                    }
                    HandleTaskView();
                }
            }
        }

        private void HandleTaskView()
        {
            GameObject taskView = Instantiate(prefab, transform.position, Quaternion.identity, content);
            taskView.GetComponent<Image>().sprite = item.Key.sprite.sprite;
            taskView.transform.GetChild(0).GetComponent<TMP_Text>().text = item.Value.type.ToString();
            taskView.transform.GetChild(1).GetComponent<TMP_Text>().text = item.Key.nameTask.ToString();
        }

        public void CloseCalendar()
        {
            selected.SetActive(false);
            noselected.SetActive(false);
            ResetCalendar();
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
        {
            int index = 0;
            foreach (TaskType item in GameManager.GetManager().calendarController.calendarInformation.Keys)
            {
                index++;
                if (item == tipe)
                {
                    content.GetChild(index).GetComponent<Image>().color = Color.green;
                    return;
                }
            }

        }
    }
}
