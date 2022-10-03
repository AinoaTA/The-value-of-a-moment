using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BitterControl : MonoBehaviour
{
    [Header("Bitter")]
    [SerializeField] GameObject content;
    [SerializeField] Bitter bitterPrefab;
    [SerializeField] TextAsset bitterProfiles;
    [SerializeField] List<string> profileBitterNames;

    [SerializeField] GameObject readBitters;
    [SerializeField] GameObject writeBitter;
    [Header("Values")]
    [SerializeField] Vector2 randomBitterTimes;
    int random;
    private void Awake()
    {
        var file = Resources.Load<TextAsset>("ProfileBitterNames");
        var content = file.text;
        var AllWords = content.Split('\n');
        profileBitterNames = new List<string>(AllWords);

        random = (int)Random.Range(randomBitterTimes.x, randomBitterTimes.y);

        CreateBit();
    }




    public void CreateBit()
    {
        for (int i = 0; i < random; i++)
        {
            Bitter bit = Instantiate(bitterPrefab, transform.position, Quaternion.identity, content.transform);
            bit.arroba.text = GetNickname(); ;
            bit.arroba.text.Replace("\r", "");
            bit.message.text = GetText();

        }

    }

    #region gets
    string GetNickname() => profileBitterNames[Random.Range(0, profileBitterNames.Count)];


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

    public void ClearDay()
    { }
}

