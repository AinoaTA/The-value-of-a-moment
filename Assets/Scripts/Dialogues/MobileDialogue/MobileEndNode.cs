using UnityEngine;

[CreateAssetMenu(fileName = "DoNothing", menuName = "Dialogue/EndNodes/DoNothing", order = 1)]
public class MobileEndNode : MobileDialogueNode
{
    public virtual void OnChosen(GameObject talker)
    {

    }
}

