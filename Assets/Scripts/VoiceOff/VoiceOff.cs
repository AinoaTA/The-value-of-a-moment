using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Dialogues/VoiceOff", order = 1)]
public class VoiceOff : ScriptableObject
{
    public AudioClip voice;
    public string text;
}
