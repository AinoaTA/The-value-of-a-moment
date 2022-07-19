using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    private float timer;
    private int RandomValue;
    public bool CurrentNotRead;

    public NotificationProfile troll, person;
    public TMP_Text Desc;
    public TMP_Text name;
    public Image ProfilePic;


    public delegate void SoundMessageDelegate();
    public static SoundMessageDelegate MessageDelegate;
    
    void Start()
    {
        GameManager.GetManager().NotificationController = this;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A) && !CurrentNotRead)
    //        SendNotification();

    //    timer += Time.deltaTime;
    //    if (timer >= 15f && !CurrentNotRead && GameManager.GetManager().CurrentStateGame==GameManager.StateGame.GamePlay)
    //        SendNotification();
    //}
    private void SendNotification()
    {
        CurrentNotRead = true;
        if(GameManager.GetManager().CanvasManager.NotificationCanvas) GameManager.GetManager().CanvasManager.NotificationCanvas.SetActive(true);
        RandomValue = Random.Range(0, 2); //frase random elegida.

        MessageDelegate?.Invoke();


        if (RandomValue == 0)
            SetTroll();
        else
            SetPeople();
    }

    private void SetTroll()
    {
        troll.current = true;
        person.current = false;
        name.text = troll.NameProfile[0];
        RandomValue = Random.Range(0, troll.Phrases.Length); //frase random elegida.
        Desc.text = troll.Phrases[RandomValue];
        RandomValue = Random.Range(0, troll.ProfilePic.Length);
        ProfilePic.sprite = troll.ProfilePic[RandomValue];
    }

    private void SetPeople()
    {
        troll.current = false;
        person.current = true;
        RandomValue = Random.Range(0, person.NameProfile.Length);
        name.text = person.NameProfile[RandomValue];
        RandomValue = Random.Range(0, person.Phrases.Length); //frase random elegida.
        Desc.text = person.Phrases[RandomValue];
        RandomValue = Random.Range(0, person.ProfilePic.Length);
        ProfilePic.sprite = person.ProfilePic[RandomValue];
    }


    public void ReadOption()
    {
        GameManager.GetManager().CanvasManager.NotificationMessage.SetActive(false);
        GameManager.GetManager().CanvasManager.MessageOpen.SetActive(true);
        CurrentNotRead = false;

        float l_confident;
        if (troll.current)
        {
            l_confident = Random.Range(troll.minCofindent, troll.maxConfident);
            GameManager.GetManager().Autocontrol.RemoveAutoControl(l_confident);
        }
        else
        {
            l_confident = Random.Range(person.minCofindent, person.maxConfident);
            GameManager.GetManager().Autocontrol.AddAutoControl(l_confident);
        }
            
        timer = 0;
    }

    public void DeleteOption()
    {
        timer /= 2;
        GameManager.GetManager().CanvasManager.MessageOpen.SetActive(false);
        GameManager.GetManager().CanvasManager.NotificationMessage.SetActive(false);

        CurrentNotRead = false;
    }
}
