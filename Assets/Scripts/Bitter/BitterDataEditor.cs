using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BitterData))]
public class BitterDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Load Data"))
        {
            BitterData bitterData = (BitterData)target;
            var file = Resources.Load<TextAsset>("Bitter/ProfileBitterNamesGood");
            var file2 = Resources.Load<TextAsset>("Bitter/ProfileBitterNamesBad");
            var file3 = Resources.Load<TextAsset>("Bitter/BitterMessagesGood");
            var file4 = Resources.Load<TextAsset>("Bitter/BitterMessagesBad");

            var content = file.text;
            var content2 = file2.text;
            var content3 = file3.text;
            var content4 = file4.text;

            var AllWords = content.Split('\n');
            var AllWords2 = content2.Split('\n');
            var AllWords3 = content3.Split('\n');
            var AllWords4 = content4.Split('\n');

            bitterData.profileBitterNamesGood = new List<string>(AllWords);
            bitterData.profileBitterNamesBad = new List<string>(AllWords2);
            bitterData.BitterMessagesGood = new List<string>(AllWords3);
            bitterData.BitterMessagesBad = new List<string>(AllWords4);

            EditorUtility.SetDirty(bitterData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
