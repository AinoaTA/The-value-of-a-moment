using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SpaceCalendar : MonoBehaviour
{
    public enum SpaceType { Mañana, MedioDia, Tarde, Noche }
    public SpaceType type;

    public List<TaskType> taskSave = new List<TaskType>();
    public int maxTaskSaved=4;
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.childCount>=maxTaskSaved)
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

    public bool ThereIsSpace(){  return gameObject.transform.childCount < maxTaskSaved;} 
}
