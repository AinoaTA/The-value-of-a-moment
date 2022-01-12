using UnityEngine.UI;
using UnityEngine;

public class Autocontrol : MonoBehaviour
{
    private float maxValue = 100f;

    private float m_currentValue = 50; //modificado solo por función.
  //  public float currentValue => m_currentValue;
    public Slider m_Slider;

    private void Awake()
    {
        GameManager.GetManager().SetAutocontrol(this);
    }

    private void Start()
    {
        m_Slider.value = m_currentValue / maxValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            AddAutoControl(10);
        if (Input.GetKeyDown(KeyCode.T))
            RemoveAutoControl(10);
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
