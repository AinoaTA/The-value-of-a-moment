using UnityEngine.UI;
using UnityEngine;

public class Autocontrol : MonoBehaviour
{
    private float maxValue = 100f;

    private float m_currentValue = 20;
    public Slider m_Slider;

    private void Start()
    {
        GameManager.GetManager().Autocontrol = this;
        m_Slider.value = m_currentValue / maxValue;
    }

    public void AddAutoControl(float value)
    {
        m_currentValue += value;
        if (m_currentValue > 100)
            m_currentValue = 100;
        m_Slider.value = m_currentValue / maxValue;
    }

    public void RemoveAutoControl(float value)
    {
        m_currentValue -= value;
        if (m_currentValue < 0)
            m_currentValue= 0;

        m_Slider.value = m_currentValue / maxValue;
    }
}
