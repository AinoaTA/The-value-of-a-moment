using UnityEngine;


[CreateAssetMenu(fileName = "BittersMessages", menuName = "Bitter", order = 1)]
public class Bitters : ScriptableObject
{
    public string nameBitter;
    public int day;
    public int requiredAutocontrol;
    public string[] possibleBitters;
    public bool conditionComplete;
}
