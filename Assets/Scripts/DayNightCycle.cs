using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public enum DayState { D, M, T, N }

    [Tooltip("D=Mañana, M=Mediodia, T=Tarde, N=Noche")]
    public DayState m_DayState;

    private Animator m_Anims;

    private void Start()
    {
        m_Anims = GetComponent<Animator>();
        ChangeDay(m_DayState);
    }
    public void ChangeDay(DayState newState)
    {
        m_Anims.SetInteger("time", (int)newState);

        //switch (m_DayState)
        //{
        //    case DayState.D:
                
        //        break;
        //    case DayState.M:
        //        break;
        //    case DayState.T:
        //        break;
        //    case DayState.N:
        //        break;
        //    default:
        //        break;
        //}
       

        m_DayState = newState;
    }
}
