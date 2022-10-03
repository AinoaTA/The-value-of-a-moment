using System.Collections.Generic;
using UnityEngine;

public class BitterControl : MonoBehaviour
{
    public GameObject content;
    public Bitter bitterPrefab;
    public TextAsset bitterProfiles;
    public List<string> profileBitterNames;

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
}

