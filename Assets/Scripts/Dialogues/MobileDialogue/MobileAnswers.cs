using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "MobileAnswers/NewAnswers", order = 1)]
public class MobileAnswers : ScriptableObject
{
    public bool started, finished;
    [Tooltip("Cada chat g�nerico (grupo, mama i ex) tiene su propia id")]
    public string id;
    [Tooltip("Orden de la conversaci�n teniendo en cuenta la id")]
    public int index_id;
    public string content;

    public MobileRepplies nextRepplies;
}
