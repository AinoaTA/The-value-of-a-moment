using System.Collections.Generic;
using UnityEngine;

namespace Calendar
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class SpaceCalendar : MonoBehaviour
    {
        [HideInInspector] public string[] timeName = { "Mañana", "Medio día", "Tarde", "Noche" };

        public enum SpaceType { Manana, MedioDia, Tarde, Noche }
        public SpaceType type;

        public DayController.DayTime timeDate;

        public List<TaskType> taskSave = new List<TaskType>();
        [SerializeField] private int maxTaskSaved = 4;
        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.transform.childCount >= maxTaskSaved)
                return;
            TaskType task = other.GetComponent<TaskType>();

            if (!taskSave.Contains(task))
            {
                task.InAnySpaceCalendar = true;
                task.calendar = this;
                taskSave.Add(task);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!ThereIsSpace())
                return;

            TaskType task = other.GetComponent<TaskType>();
            task.InAnySpaceCalendar = false;
            task.calendar = null;
            taskSave.Remove(task);
        }

        public bool ThereIsSpace() { return gameObject.transform.childCount < maxTaskSaved; }
    }
}