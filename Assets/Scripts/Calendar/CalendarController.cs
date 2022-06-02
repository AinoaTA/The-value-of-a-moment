using UnityEngine;

public class CalendarController : MonoBehaviour
{
    public string nameTask;

    private void Start()
    {
        GameManager.GetManager().calendarController = this;
    }
    public void GetInfoTask(Interactables interactableType )
    { 
    
    
    }
}
