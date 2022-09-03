using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "MobileAnswers/NewRepply", order = 1)]
public class MobileRepplies : ScriptableObject
{
    public Conversation[] conver;
    [System.Serializable]
    public struct Conversation 
    {
        public string name;
        public string answer;
    }
    public MobileAnswers[] nextText;
}
