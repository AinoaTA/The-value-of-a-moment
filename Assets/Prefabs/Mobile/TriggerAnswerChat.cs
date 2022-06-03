using UnityEngine;
using TMPro;

public class TriggerAnswerChat : MonoBehaviour
{
    [HideInInspector]public int value;
    //TEMPORAL
    public TMP_Text text;

    private void Awake()
    {//TEMPORAL
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        text.text = value.ToString();
    }
    public void Select()
    {
        GameManager.GetManager().mobile.SelectedAnswer(value);
    }
}
