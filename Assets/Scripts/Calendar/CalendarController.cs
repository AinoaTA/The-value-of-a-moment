using UnityEngine;

public class CalendarController : MonoBehaviour
{
    public string nameTask;

    public Transform TaskMovement;

    private void Start()
    {
        GameManager.GetManager().calendarController = this;
    }
    public void GetInfoTask(Interactables interactableType )
    { 
    
    
    }
}
