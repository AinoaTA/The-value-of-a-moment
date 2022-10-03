using System.Collections.Generic;
using UnityEngine;

public class BitterControl : MonoBehaviour
{
    [Header("Bitter")]
    [SerializeField] GameObject content;
    [SerializeField] Bitter bitterPrefab;
    [SerializeField] TextAsset bitterProfiles;
    [SerializeField] List<string> profileBitterNamesGood;
    [SerializeField] List<string> profileBitterNamesBad;

    [SerializeField] GameObject readBitters;
    [SerializeField] GameObject writeBitter;
    [Header("Values")]
    [SerializeField] Vector2 randomBitterTimes;
    int random;
    private void Awake()
    {
        #region read TXT
        //read the .txts names to get a lot of nicknames. this will we a scriptable object in a futureeeeee ejej 3oct2022.
        var file = Resources.Load<TextAsset>("ProfileBitterNamesGood");
        var file2 = Resources.Load<TextAsset>("ProfileBitterNamesBad");
        var content = file.text;
        var content2 = file2.text;
        var AllWords = content.Split('\n');
        var AllWords2 = content2.Split('\n');
        profileBitterNamesGood = new List<string>(AllWords);
        profileBitterNamesBad = new List<string>(AllWords2);
        random = (int)Random.Range(randomBitterTimes.x, randomBitterTimes.y);
        #endregion

        CreateBit();
    }

    /// <summary>
    /// Create bitters
    /// </summary>
    public void CreateBit()
    {
        //create a bit depends on autocontrol's player.
        for (int i = 0; i < random; i++)
        {
            Bitter bit = Instantiate(bitterPrefab, transform.position, Quaternion.identity, content.transform);
            bit.arroba.text = GetNickname();
            bit.arroba.text.Replace("\r", "");
            bit.message.text = GetText();
            //pic etc
        }
    }

    #region gets
    string GetNickname() => profileBitterNamesGood[Random.Range(0, profileBitterNamesGood.Count)];


    string GetText()
    {
        return "aaaaaa";

    }
    #endregion
    #region buttons
    public void ReadBitter()
    {
        readBitters.SetActive(true);
        writeBitter.SetActive(false);
    }

    public void WriteBitter()
    {
        readBitters.SetActive(false);
        writeBitter.SetActive(true);
    }

    #endregion
    /// <summary>
    /// Remove all bitters created
    /// </summary>
    public void ClearDay() { }
}