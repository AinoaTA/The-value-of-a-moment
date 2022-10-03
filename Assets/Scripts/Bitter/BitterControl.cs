using System.Collections.Generic;
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

    private void Awake()
    {
        var file = Resources.Load<TextAsset>("ProfileBitterNames");
        var content = file.text;
        var AllWords = content.Split('\n');
        profileBitterNames = new List<string>(AllWords);
    }




    public void CreateBit()
    {


    }


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

