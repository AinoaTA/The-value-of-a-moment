using UnityEngine;

public class Computer : Interactables
{
    [SerializeField]
    GameObject computerScreen, programScreen,
        calendarMaterialScreen, emailScreenMaterial, email;


    [SerializeField] Transform computerPos;

    [SerializeField] GameObject programMinigame;
    bool anyButtonScreenActive;


    private void Start()
    {
        GameManager.GetManager().computer = this;
    }
    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                //FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Env/PC/On", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                ComputerON();
                GameManager.GetManager().canvasController.ComputerScreenIn();
                switch (GameManager.GetManager().dayController.GetDayNumber())
                {
                    case DayController.Day.one:
                        GameManager.GetManager().dialogueManager.SetDialogue("IOrdenador");
                        break;
                    case DayController.Day.two:
                        GameManager.GetManager().dialogueManager.SetDialogue("D2AccTrab_PCRevisar");
                        break;
                    case DayController.Day.three:
                        break;
                    case DayController.Day.fourth:
                        break;
                    default:
                        break;
                }
               
                break;
        }
    }

    public override void ExitInteraction()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom Out", transform.position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/PC/Off", transform.position);
        GameManager.GetManager().canvasController.ComputerScreenOut();
        GameManager.GetManager().StartThirdPersonCamera();
        if (anyButtonScreenActive)
        {
            GameManager.GetManager().programMinigame.QuitMiniGame();
            GameManager.GetManager().calendarController.BackCalendar();
            GameManager.GetManager().emailController.ShowEmail(false);
        }

        ComputerOFF();
        base.ExitInteraction();
    }

    #region (des)-active gameObects
    public void ComputerON()
    {
        GameManager.GetManager().playerController.SetAnimation("Computer",computerPos);
        computerScreen.SetActive(true);
        programScreen.SetActive(false);
        programMinigame.SetActive(false);
        programMinigame.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        anyButtonScreenActive = false;
        email.SetActive(true);
        emailScreenMaterial.SetActive(false);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerOFF()
    {
        GameManager.GetManager().playerController.TemporalExit();
        computerScreen.SetActive(false);
        programScreen.SetActive(false);
        programMinigame.SetActive(false);
        programMinigame.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(false);
        anyButtonScreenActive = false;
        email.SetActive(false);
        if (!GameManager.GetManager().programmed && (int)GameManager.GetManager().dayController.dayState > 1)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("Atardece", delegate
            {
                GameManager.GetManager().blockController.UnlockAll(DayController.DayTime.Noche);
                GameManager.GetManager().blockController.Unlock("Bed");
                GameManager.GetManager().blockController.Unlock("Window");
            });
        
        }
    }

    public void ComputerCalendar()
    {
        if (anyButtonScreenActive) return;
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTrab_AgendaPlan");
                break;
            default: break;
        }
        anyButtonScreenActive = true;
        GameManager.GetManager().calendarController.ShowCalendar();
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(true);
        emailScreenMaterial.SetActive(false);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerProgram()
    {
        if (anyButtonScreenActive) return;
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                GameManager.GetManager().dialogueManager.SetDialogue("PCProgramar");
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTrab_PCProgram");
                break;
            default: break;
        }
        programMinigame.SetActive(true);
        anyButtonScreenActive = true;
        programScreen.SetActive(true);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(false);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerEmail()
    {
        if (anyButtonScreenActive) return;
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                GameManager.GetManager().dialogueManager.SetDialogue("PCMirarMail");
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccTrab_PCMirarMail");
                break;
            default: break;
        }
        anyButtonScreenActive = true;
        GameManager.GetManager().emailController.ShowEmail(true);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(true);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }
    #endregion
}
