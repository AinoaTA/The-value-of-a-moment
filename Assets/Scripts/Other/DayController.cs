using System.Collections;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public enum DayTime { Manana, MedioDia, Tarde, Noche }
    public enum Day { one, two, three, fourth }
    public DayTime dayState;
    public Day currentDay;
    private int counter;
    [SerializeField] int maxTasks = 5;
    private Animator anims;
    private int counterTaskDay = 0;

    private void Awake()
    {
        GameManager.GetManager().dayController = this;
        anims = GetComponent<Animator>();
    }

    private void Start()
    {
        counter = (int)dayState;
        ChangeDay(0);
    }
    public void ChangeDay(int newState)
    {
        print("ME ESTAS JODIENDO");
        anims.SetInteger("time", (int)newState);
        dayState = (DayTime)newState;
        counter = 0;
        switch (dayState)
        {
            case DayTime.Manana:
                break;
            case DayTime.MedioDia:
                break;
            case DayTime.Tarde:
                StartCoroutine(Delay());
                break;
            case DayTime.Noche:
                break;
            default:
                break;
        }
    }

    IEnumerator Delay() 
    {
        yield return new WaitWhile(()=>GameManager.GetManager().dialogueManager.waitDialogue);
        GameManager.GetManager().dialogueManager.SetDialogue("PonerseATrabajar",
                      delegate
                      {
                          GameManager.GetManager().blockController.UnlockAll(DayTime.Tarde);
                          GameManager.GetManager().blockController.Unlock("Window");
                      });
    }
    public void NewDay()
    {
        counter = 0;
        counterTaskDay = 0;
        ChangeDay(counter);
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
            case Day.fourth:
                //GameManager.GetManager().emailController.Recieve();
                break;
            default:
                break;
        }
    }

    public void TaskDone()
    {
        counterTaskDay++;
        if (dayState == DayTime.Noche)
            GameManager.GetManager().dialogueManager.SetDialogue("Anochece", canRepeat:true);

        print(counterTaskDay + " nuevo stado" + (counterTaskDay % 5 == 0));
        if (counterTaskDay >= maxTasks)
        {
            if (counter < 4) counter++;
            else counter = 0;

            ChangeDay(counter);

        }
    }

    public DayTime GetTimeDay()
    {
        return dayState;
    }

    public Day GetDayNumber()
    {
        return currentDay;
    }

    public void NextStateDay()
    {
        currentDay++;
        ChangeDay((int)currentDay);
    }
}
