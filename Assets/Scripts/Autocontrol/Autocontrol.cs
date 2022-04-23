using UnityEngine.UI;
using UnityEngine;
using System.Collections;

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
        StartCoroutine(AddC(value));
    }

    public void RemoveAutoControl(float value)
    {
        StartCoroutine(RemoveC(value));
    }

    IEnumerator RemoveC(float value)
    {
        for (int i = 0; i < value; i++)
        {
            if (m_currentValue > 0)
                m_currentValue -= 1;

            m_Slider.value = m_currentValue / maxValue;

            yield return null;
        }


    }

    IEnumerator AddC(float value)
    {
        for (int i = 0; i < value; i++)
        {
            if (m_currentValue < maxValue)
                m_currentValue += 1;

            m_Slider.value = m_currentValue / maxValue;

            yield return null;
        }
    }
}
