using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SpaceCalendar : MonoBehaviour
{
    public enum SpaceType { morning, midnoon, afternoon, night }
    public SpaceType type;


    public List<TaskType> taskSave=new List<TaskType>();






}
