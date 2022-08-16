using TMPro;
using UnityEngine;

public class MobileDialogueManager : MonoBehaviour
{
    //public static DialogueManager Instance;

    public Animator DialogueAnimator;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI SpeechText;
    public TextMeshProUGUI[] OptionsText;

    private MobileDialogueNode _currentNode;

    private GameObject _talker;

    public void OptionChosen(int option)
    {
        //_currentNode = _currentNode.Options[option].NextNode;
        //SetText(_currentNode);

        //if (_currentNode is EndNode)
        //{
        //    DoEndNode(_currentNode as EndNode);
        //}
        //else
        //{
        //    SetText(_currentNode);
        //}

    }

    //private void DoEndNode(EndNode currentNode)
    //{
    //    currentNode.OnChosen(_talker);
    //    HideDialogue();
    //}

    private void ShowDialogue()
    {
        DialogueAnimator.SetBool("Show", true);
    }

    private void HideDialogue()
    {
        DialogueAnimator.SetBool("Show", false);
    }



    internal static void StartDialogue(MobileConversation dialogueData, GameObject talker)
    {

       // Instance._StartDialogue(dialogueData, talker);
    }

    private void _StartDialogue(MobileConversation dialogueData, GameObject talker)
    {
        _talker = talker;
        _currentNode = dialogueData.StartNode;
        NameText.text = dialogueData.Name;
        SetText(_currentNode);
        ShowDialogue();
    }

    private void SetText(MobileDialogueNode node)
    {
        SpeechText.text = node.Text;
        for (int i = 0; i < OptionsText.Length; i++)
        {
            if (i < node.Options.Count)
            {
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                OptionsText[i].text = node.Options[i].Text;
            }
            else
            {
                OptionsText[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
