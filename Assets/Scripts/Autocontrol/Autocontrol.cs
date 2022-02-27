using UnityEngine.UI;
using UnityEngine;

public class Autocontrol : MonoBehaviour
{
    private float maxValue = 100f;

    private float m_currentValue = 20; //modificado solo por función.
  //  public float currentValue => m_currentValue;
    public Slider m_Slider;

    private void Awake()
    {
        GameManager.GetManager().Autocontrol = this;
    }

    private void Start()
    {
        m_Slider.value = m_currentValue / maxValue;
    }

    private void Update()
    {
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
