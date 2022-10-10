using UnityEngine;

public class CorchoImage : MonoBehaviour
{
    public Corcho corcho;
    public string dialogue;
    public BoxCollider col;

    private void OnMouseDown()
    {
        GameManager.GetManager().dialogueManager.SetDialogue(dialogue, canRepeat:true, act: delegate 
        {
            corcho.BlockAll(true);
        });
    }

    public void Ready(bool v) 
    {
        col.enabled = v;
    }
}
