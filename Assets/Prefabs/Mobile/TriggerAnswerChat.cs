using UnityEngine;
using TMPro;

public class TriggerAnswerChat : MonoBehaviour
{
    [HideInInspector]public int value;
    //TEMPORAL
    public TMP_Text text;
    public MobileAnswers nextConver;

    private void Awake()
    {//TEMPORAL
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
       // text.text = value.ToString();
    }
    //public void Select()
    //{
    //    GameManager.GetManager().mobile.SelectedAnswer(value);
    //}

    public void Select()
    {
        GameManager.GetManager().mobile.SelectAnswer(nextConver);
    }
}
