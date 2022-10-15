using TMPro;

public class Cascos : GeneralActions
{
    public FMODMusic music;
    public TextMeshProUGUI textDisplay;

    private bool isMusicPlaying = false;

    private void Start()
    {
        textDisplay.text = "[E] Encender";
    }
    int one;
    public override void EnterAction()
    {
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(this);
        if (one == 0)
        {
            one++;
            GameManager.GetManager().dayController.TaskDone();
        }
        TurnMusic();
        ChangeText();
    }

    public override void ResetObject()
    {
        one = 0;
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
