using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "MobileAnswers/NewAnswers", order = 1)]
public class MobileAnswers : ScriptableObject
{
    [Header("ANSWER TYPE")]
    public bool finished;
    //[Tooltip("Orden de la conversación teniendo en cuenta la id")]
    //public int index_id;
    public string content;
    [Tooltip("Solo si se necesitan mensajes antes de empezar la conversación")]
    public MobileRepplies previusRepply;
    public MobileRepplies nextRepplies;
}
