using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Nodes", order = 1)]
public class MobileDialogueNode : ScriptableObject
{
        public List<MobileDialogueOptions> Options;
        public string Text;
}
