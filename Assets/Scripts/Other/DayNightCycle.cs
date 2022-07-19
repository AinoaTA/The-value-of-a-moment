using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public enum DayState { Mañana, MedioDia, Tarde, Noche }

    public DayState m_DayState;
    private int counter;

    private Animator m_Anims;
    private int counterTaskDay=0;

    private void Awake()
    {
        m_Anims = GetComponent<Animator>();
    } 

    private void Start()
    {
        GameManager.GetManager().dayNightCycle = this;
        counter = (int)m_DayState;
        ChangeDay(m_DayState);
    }
    public void ChangeDay(DayState newState)
    {
        m_Anims.SetInteger("time", (int)newState);
        m_DayState = newState;
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
