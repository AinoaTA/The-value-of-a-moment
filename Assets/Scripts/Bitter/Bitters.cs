using UnityEngine;


[CreateAssetMenu(fileName = "BittersMessages", menuName = "Bitter", order = 1)]
public class Bitters : ScriptableObject
{
    public string nameBitter;
    public int day;
    public int requiredAutocontrol;

    public bittersText[] possibleBitters;
    [System.Serializable]
    public struct bittersText
    {
        public string resumeName;
        public string bitter;
    }

    public bool conditionComplete;
}
