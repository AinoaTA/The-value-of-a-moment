using UnityEngine;

public class DayController : MonoBehaviour
{
    public enum DayTime { Manana, MedioDia, Tarde, Noche }
    public enum Day { one, two , three }
    [SerializeField] DayTime dayState;
    [SerializeField] Day currentDay;
    private int counter;

    private Animator anims;
    private int counterTaskDay = 0;

    private void Awake()
    {
        anims = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.GetManager().dayNightCycle = this;
        counter = (int)dayState;
        ChangeDay(dayState);
    }
    public void ChangeDay(DayTime newState)
    {
        anims.SetInteger("time", (int)newState);
        dayState = newState;
    }

    public void NewDay()
    {
        counter = 0;
        counterTaskDay = 0;
        ChangeDay((DayTime)counter);
        //next day
        currentDay++;
        switch (currentDay)
        {
            case Day.one:
                break;
            case Day.two:
                break;
            case Day.three:
                break;
            default:
                break;
        }
    }

    public void TaskDone()
    {
        counterTaskDay++;
        if (counterTaskDay % 2 == 0)
        {
            counter = counter < 4 ? counter + 1 : 0;
            ChangeDay((DayTime)counter);
        }
    }

    public DayTime GetTimeDay() 
    {
        return dayState;
    }
}
