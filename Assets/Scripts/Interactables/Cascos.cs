using TMPro;
using UnityEngine;

public class Cascos : GeneralActions
{
    public FMODMusic music;
    public TextMeshProUGUI textDisplay;

    private bool isMusicPlaying = false;

    private void Start()
    {
        textDisplay.text = "[E] Encender";
    }

    public override void EnterAction()
    {
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        TurnMusic();
        ChangeText();
    }

    public override void ResetObject()
    {
        base.ResetObject();
    }

    private void TurnMusic()
    {
        //Activar musica FMOD
    }

    private void ChangeText()
    {
        if (isMusicPlaying)
        {
            textDisplay.text = "[E] Apagar";
        }
        else
        {
            textDisplay.text = "[E] Encender";
        }
    }
}
