using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public float m_Counter;

    public float ScaleValue = 0.004f;
    private Vector3 m_InitialScale;
    private Vector3 m_ScaleWakeUpButton;
    private Vector3 m_NewScaleWakeUpButton;
    private Vector3 m_WakeScale;
    public bool blocker;
    private void Awake()
    {
        //current = GetComponent<ButtonTrigger>();
    }
    private void Start()
    {
        m_ScaleWakeUpButton = transform.localScale;
        m_InitialScale = transform.localScale;

        m_NewScaleWakeUpButton = new Vector3(ScaleValue, ScaleValue, ScaleValue);
        m_NewScaleWakeUpButton += m_ScaleWakeUpButton;
    }

    //WAKE UP BUTTON    
    public void LessEscaleWakeUp()
    {
        transform.localScale = m_ScaleWakeUpButton;

        gameObject.transform.localScale -= new Vector3(ScaleValue, ScaleValue, ScaleValue);
        m_ScaleWakeUpButton = gameObject.transform.localScale;

        if (m_Counter < 3)
            m_NewScaleWakeUpButton = m_ScaleWakeUpButton + new Vector3(0.001f, 0.001f, 0.001f);

        m_Counter++;
    }

    //BOTH
    public void ButtonEnterBoth()
    {
        transform.localScale = m_NewScaleWakeUpButton;
    }

    public void ButtonExitBoth()
    {
        if (m_Counter >= 4)
        {
            m_ScaleWakeUpButton = m_InitialScale;
            m_NewScaleWakeUpButton = m_ScaleWakeUpButton + new Vector3(ScaleValue, ScaleValue, ScaleValue);
        }

        else
            transform.localScale = m_ScaleWakeUpButton;
    }


    public void OnClicOver()
    {
        if (blocker) return;
        transform.localScale = new Vector3(ScaleValue + m_InitialScale.x, ScaleValue + m_InitialScale.y);
    }

    public void OnClickExit()
    {
        transform.localScale = m_InitialScale;
    }
}
