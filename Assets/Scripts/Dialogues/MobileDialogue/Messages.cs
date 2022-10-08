using TMPro;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text content;
    public MobileAnswers mobileAnswer;
    [HideInInspector] public int id_index = 0;

    public void Select()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/UI/Phone Enter");
        GameManager.GetManager().mobile.SelectAnswer(mobileAnswer);
    }
}
