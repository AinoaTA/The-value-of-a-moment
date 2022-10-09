using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "MobileAnswers/NewRepply", order = 1)]
public class MobileRepplies : ScriptableObject
{
    public enum Names { Zoe = 0, Ari, Elle, Mama }
    [Header("REPPLY TYPE")]
    public Conversation[] conver;
    [System.Serializable]
    public struct Conversation
    {
        public Names names;
        public string answer;
    }
    public MobileAnswers[] nextText;
}
