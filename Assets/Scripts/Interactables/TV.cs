using UnityEngine;

public class TV : Interactables
{
    public GameObject screen;
    // handle de canales
    private int[] channels;
    private int currChannel;    // Esto permite guardar el canal al apagar la tele, como irl

    private void Start()
    {
        screen.SetActive(false);
        currChannel = 0;
    }

    public override void Interaction(int optionNumber)
    {
        base.Interaction(optionNumber);
        switch (optionNumber)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/TVOn", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                GameManager.GetManager().cameraController.StartInteractCam(nameInteractable);
                screen.SetActive(true);

                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        switch (GameManager.GetManager().dayController.GetTimeDay())
                        {
                            case DayController.DayTime.Manana:
                                break;
                            case DayController.DayTime.MedioDia:
                                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                                break;
                            case DayController.DayTime.Tarde:
                                GameManager.GetManager().dialogueManager.SetDialogue("ICocina", canRepeat: true);
                                break;
                            case DayController.DayTime.Noche:
                                GameManager.GetManager().dialogueManager.SetDialogue("Anochece", canRepeat: true);
                                break;
                            default:
                                break;
                        }
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccDescRelax_TV");
                        GameManager.GetManager().IncrementInteractableCount();
                        break;
                    case DayController.Day.three:
                        break;
                    case DayController.Day.fourth:
                        break;
                }


                InteractableBlocked = true;
                GameManager.GetManager().dayController.TaskDone();
                break;
        }
    }

    public override void ExitInteraction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/TVOff", transform.position);
        screen.SetActive(false);
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    public void ChangeChannel()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/TV SwitchCh", transform.position);
        // 1 funci�n para subir o bajar canal o 2 funciones
        currChannel++;
        // Set screen to channels[currChannel]
    }
}
