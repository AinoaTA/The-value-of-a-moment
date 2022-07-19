using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public enum DayState { Mañana, MedioDia, Tarde, Noche }

    public DayState DayState;
    private int counter;

    private Animator Anims;
    private int counterTaskDay=0;

    private void Awake()
    {
        Anims = GetComponent<Animator>();
    } 

    private void Start()
    {
        GameManager.GetManager().dayNightCycle = this;
        counter = (int)DayState;
        ChangeDay(DayState);
    }
    public void ChangeDay(DayState newState)
    {
        Anims.SetInteger("time", (int)newState);
        DayState = newState;
    }

    public void NewDay()
    {

        counter = 0;
        counterTaskDay = 0;
        ChangeDay((DayState)counter);
    }

    public void TaskDone()
    {
        counterTaskDay++;
        if (counterTaskDay % 2 == 0)
        {
            counter = counter < 4 ? counter + 1 : 0;
            ChangeDay((DayState)counter);
        }
    }
}
