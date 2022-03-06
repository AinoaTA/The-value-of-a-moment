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


    public delegate void SoundMessageDelegate();
    public static SoundMessageDelegate m_MessageDelegate;
    
    private void Awake()
    {
        GameManager.GetManager().NotificationController = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !m_CurrentNotRead)
            SendNotification();

        timer += Time.deltaTime;
        if (timer >= 15f && !m_CurrentNotRead && GameManager.GetManager().m_CurrentStateGame==GameManager.StateGame.GamePlay)
            SendNotification();
    }
    private void SendNotification()
    {
        m_CurrentNotRead = true;
        if(GameManager.GetManager().CanvasManager.m_NotificationCanvas) GameManager.GetManager().CanvasManager.m_NotificationCanvas.SetActive(true);
        m_RandomValue = Random.Range(0, 2); //frase random elegida.

        m_MessageDelegate?.Invoke();


        if (m_RandomValue == 0)
            SetTroll();
        else
            SetPeople();
    }

    private void SetTroll()
    {
        m_troll.current = true;
        m_person.current = false;
        m_name.text = m_troll.m_NameProfile[0];
        m_RandomValue = Random.Range(0, m_troll.m_Phrases.Length); //frase random elegida.
        m_Desc.text = m_troll.m_Phrases[m_RandomValue];
        m_RandomValue = Random.Range(0, m_troll.m_ProfilePic.Length);
        m_ProfilePic.sprite = m_troll.m_ProfilePic[m_RandomValue];
    }

    private void SetPeople()
    {
        m_troll.current = false;
        m_person.current = true;
        m_RandomValue = Random.Range(0, m_person.m_NameProfile.Length);
        m_name.text = m_person.m_NameProfile[m_RandomValue];
        m_RandomValue = Random.Range(0, m_person.m_Phrases.Length); //frase random elegida.
        m_Desc.text = m_person.m_Phrases[m_RandomValue];
        m_RandomValue = Random.Range(0, m_person.m_ProfilePic.Length);
        m_ProfilePic.sprite = m_person.m_ProfilePic[m_RandomValue];
    }


    public void ReadOption()
    {
        GameManager.GetManager().CanvasManager.NotificationMessage.SetActive(false);
        GameManager.GetManager().CanvasManager.MessageOpen.SetActive(true);
        m_CurrentNotRead = false;

        float l_confident;
        if (m_troll.current)
        {
            l_confident = Random.Range(m_troll.minCofindent, m_troll.maxConfident);
            GameManager.GetManager().Autocontrol.RemoveAutoControl(l_confident);
        }
        else
        {
            l_confident = Random.Range(m_person.minCofindent, m_person.maxConfident);
            GameManager.GetManager().Autocontrol.AddAutoControl(l_confident);
        }
            
        timer = 0;
    }

    public void DeleteOption()
    {
        timer /= 2;
        GameManager.GetManager().CanvasManager.MessageOpen.SetActive(false);
        GameManager.GetManager().CanvasManager.NotificationMessage.SetActive(false);

        m_CurrentNotRead = false;
    }
}
