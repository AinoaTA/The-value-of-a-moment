using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitterControl : MonoBehaviour
{
    [Header("Profiles")]
    [SerializeField] Sprite elleProfile;
    [SerializeField] Sprite[] profileAnonymous;
    [SerializeField] Sprite[] normalPeople;
    [SerializeField] int addAutocontrol = 5;
    [SerializeField] Vector2 removeAutocontrol = new Vector2(2, 5);


    [Header("Bitter")]
    [SerializeField] GameObject content;
    [SerializeField] GameObject warning;
    [SerializeField] Bitter bitterPrefab;
    [SerializeField] TextAsset bitterProfiles;
    [SerializeField] BitterData bitterData;
    List<string> profileBitterNamesGood;
    List<string> profileBitterNamesBad;
    List<string> BitterMessagesBad;
    List<string> BitterMessagesGood;

    [SerializeField] GameObject readBitters;
    [SerializeField] WriteBitter writeBitter;
    [Header("Values")]
    [SerializeField] Vector2 randomBitterTimes;
    int random;
    Bitter playerBit;

    private void Awake()
    {
        profileBitterNamesGood = new List<string>(bitterData.profileBitterNamesGood);
        profileBitterNamesBad = new List<string>(bitterData.profileBitterNamesBad);
        BitterMessagesBad = new List<string>(bitterData.BitterMessagesBad);
        BitterMessagesGood = new List<string>(bitterData.BitterMessagesGood);

        random = (int)Random.Range(randomBitterTimes.x, randomBitterTimes.y);
        WriteBitter();
    }

    #region gets
    string GetNickname(List<string> list) => list[Random.Range(0, list.Count)];

    string GetText(List<string> list)
    {
        int r = Random.Range(0, list.Count);
        string save = list[r];
        list.RemoveAt(r);

        return save;
    }

    Sprite GetPic(Sprite[] list) => list[Random.Range(0, list.Length)];

    #endregion

    public void Open()
    {
        gameObject.SetActive(true);
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                GameManager.GetManager().dialogueManager.SetDialogue("PCRedesSociales");
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTelef_Bitter");
                break;
            default: break;
        }
    }

    #region buttons
    public void ReadBitter()
    {
        warning.SetActive(content.transform.childCount == 0);
        readBitters.SetActive(true);
        writeBitter.gameObject.SetActive(false);
    }

    public void WriteBitter()
    {
        readBitters.SetActive(false);
        writeBitter.gameObject.SetActive(true);

        writeBitter.WriteBit();
    }

    public void SendBitter(string text)
    {
        ReadBitter();
        warning.SetActive(false);
        Bitter bit = Instantiate(bitterPrefab, transform.position, Quaternion.identity, content.transform);
        bit.arroba.text = "Elle_DevRoomer";
        bit.UpdateFavsPlayer();
        bit.message.text = text;
        bit.profilePic.sprite = elleProfile;
        playerBit = bit;
        StartCoroutine(CreateBitRoutine());
    }

    IEnumerator CreateBitRoutine()
    {
        int rnd;
        //create a bit. Is random but affects to autocontrol's player 
        for (int i = 0; i < random; i++)
        {
            rnd = Random.Range(0, 2);

            Bitter bit = Instantiate(bitterPrefab, transform.position, Quaternion.identity, content.transform);
            bit.isBad = rnd != 0;
            bit.UpdateFavs();
            bit.arroba.text = rnd == 0 ? GetNickname(profileBitterNamesGood) : GetNickname(profileBitterNamesBad);
            bit.message.text = rnd == 0 ? GetText(BitterMessagesGood) : GetText(BitterMessagesBad);
            bit.profilePic.sprite = rnd == 0 ? GetPic(normalPeople) : GetPic(profileAnonymous);

            if (rnd == 0)//is a good answer // new check
                GameManager.GetManager().autocontrol.AddAutoControl(addAutocontrol);
            else
                GameManager.GetManager().autocontrol.RemoveAutoControl(Random.Range(removeAutocontrol.x, removeAutocontrol.y));

            yield return new WaitForSeconds(1f);
            //Debug.Log("Manu posible sonido? sonido como de escribir del facebookmessener que hace trurur tururu la burbuja");
        }
    }

    public void ExitBitter()
    {
        ReadBitter();
        gameObject.SetActive(false);
    }
    #endregion

    /// <summary>
    /// Remove all bitters created
    /// </summary>
    public void ClearDay() { }
}