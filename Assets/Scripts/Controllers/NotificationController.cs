using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    private float timer;
    private int m_RandomValue;
    public bool m_CurrentNotRead;

    public NotificationProfile m_troll, m_person;
    public TMP_Text m_Desc;
    public TMP_Text m_name;
    public Image m_ProfilePic;

    private void Awake()
    {
        GameManager.GetManager().SetNotificationController(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !m_CurrentNotRead)
            SendNotification();

        timer += Time.deltaTime;
        print(timer);
        if (timer >= 15f && !m_CurrentNotRead && !GameManager.GetManager().GetCanvasManager().ScreenActivated())
            SendNotification();
    }
    private void SendNotification()
    {
        m_CurrentNotRead = true;
        GameManager.GetManager().GetCanvasManager().m_NotificationCanvas.SetActive(true);
        m_RandomValue = Random.Range(0, 2); //frase random elegida.

        if (m_RandomValue == 0)
            SetTroll();
        else
            SetPeople();
    }

    private void SetTroll()
    {
        m_troll.current = true;
        m_person.current = false;
        m_name.text = m_troll.m_Name[0];
        m_RandomValue = Random.Range(0, m_troll.m_Phrases.Length); //frase random elegida.
        m_Desc.text = m_troll.m_Phrases[m_RandomValue];
        m_ProfilePic.sprite = m_troll.m_ProfilePic;
    }

    private void SetPeople()
    {
        m_troll.current = false;
        m_person.current = true;
        m_RandomValue = Random.Range(0, m_person.m_Name.Length);
        m_name.text = m_person.m_Name[m_RandomValue];
        m_RandomValue = Random.Range(0, m_person.m_Phrases.Length); //frase random elegida.
        m_Desc.text = m_person.m_Phrases[m_RandomValue];
        m_ProfilePic.sprite = m_person.m_ProfilePic;
    }


    public void ReadOption()
    {
        GameManager.GetManager().GetCanvasManager().NotificationMessage.SetActive(false);
        GameManager.GetManager().GetCanvasManager().MessageOpen.SetActive(true);
        m_CurrentNotRead = false;

        float l_confident;
        if (m_troll.current)
        {
            l_confident = Random.Range(m_troll.minCofindent, m_troll.maxConfident);
            GameManager.GetManager().GetAutoControl().RemoveAutoControl(l_confident);
        }
        else
        {
            l_confident = Random.Range(m_person.minCofindent, m_person.maxConfident);
            GameManager.GetManager().GetAutoControl().AddAutoControl(l_confident);
        }
            
        timer = 0;
    }

    public void DeleteOption()
    {
        timer /= 2;
        GameManager.GetManager().GetCanvasManager().MessageOpen.SetActive(false);
        GameManager.GetManager().GetCanvasManager().NotificationMessage.SetActive(false);

        m_CurrentNotRead = false;
    }
}
