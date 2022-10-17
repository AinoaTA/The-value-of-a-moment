using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR



[CustomEditor(typeof(DialoguesList))]
public class DialoguesListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Load Data"))
        {
            DialoguesList dialoguesList = (DialoguesList)target;
            // if (dialoguesList.voicesPath == "") dialoguesList.voicesPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(dialoguesList)) + "\\voices\\";

            DialogueJSON dialogueList = JsonUtility.FromJson<DialogueJSON>(dialoguesList.dialoguesFile.text);


            Dictionary<string, DialogueJSON> dialogues = new Dictionary<string, DialogueJSON>();
            foreach (DialogueLineJSON line in dialogueList.lines)
            {
                if (line.dialogue == null) line.dialogue = "";
                line.played = false;
                if (!dialogues.ContainsKey(line.dialogue))
                {
                    dialogues.Add(line.dialogue, new DialogueJSON());
                    dialogues[line.dialogue].id = line.dialogue;
                }
                dialogues[line.dialogue].lines.Add(line);

                //  line.voice_es = LoadVoice(dialoguesList.voicesPath + line.ID + ".mp3");
                //  line.voice_en = LoadVoice(dialoguesList.voicesPathEn + line.ID + ".mp3");
            }

            dialoguesList.dialogues = new List<DialogueJSON>();
            foreach (DialogueJSON dialogue in dialogues.Values)
            {
                dialoguesList.dialogues.Add(dialogue);

            }



            EditorUtility.SetDirty(dialoguesList);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    AudioClip LoadVoice(string path)
    {
        if (System.IO.File.Exists(path))
            return AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        return null;
    }
}
#endif