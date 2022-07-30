using UnityEngine;
namespace Menu
{
    [CreateAssetMenu(fileName = "Options", menuName = "ScriptableObject/OptionsData", order = 1)]

    public class OptionsData : ScriptableObject
    {
        [Range(0.0001f, 1)] public float music;
        [Range(0.0001f, 1)] public float voices;
        [Range(0.0001f, 1)] public float sfx;
    }
}
