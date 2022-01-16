using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotificationProfile", menuName = "ScriptableObject/NotificationProfile", order = 1)]
public class NotificationProfile : ScriptableObject
{

    public Sprite[] m_ProfilePic;
    public string[] m_NameProfile;
    public string[] m_Phrases;
    public float minCofindent;
    public float maxConfident;
    public bool current;

}
