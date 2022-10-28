using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BitterData", menuName = "Bitter", order = 1)]
public class BitterData : ScriptableObject
{
    public List<string> profileBitterNamesGood;
    public List<string> profileBitterNamesBad;
    public List<string> BitterMessagesBad;
    public List<string> BitterMessagesGood;
}
