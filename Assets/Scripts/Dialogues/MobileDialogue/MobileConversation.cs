using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Conversation", menuName = "Dialogue/Conversation", order = 1)]
public class MobileConversation : ScriptableObject
{
    
    
        public string Name;
        public MobileDialogueNode StartNode;
    

}
