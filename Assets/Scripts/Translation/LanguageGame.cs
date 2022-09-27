using UnityEngine;

public static class LanguageGame 
{
    public enum Languages { ESP=0, ENG }
    public static Languages lang=Languages.ESP;

    public static void SetLanguage(int id) 
    {
        lang = (Languages)id;
        Debug.LogWarning(lang+" LANGUAGE SET");
    }
}
