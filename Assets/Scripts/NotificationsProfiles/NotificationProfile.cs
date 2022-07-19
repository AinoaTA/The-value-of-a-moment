using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotificationProfile", menuName = "ScriptableObject/NotificationProfile", order = 1)]
public class NotificationProfile : ScriptableObject
{

    public Sprite[] ProfilePic;
    public string[] NameProfile;
    public string[] Phrases;
    public float minCofindent;
    public float maxConfident;
    public bool current;

}
