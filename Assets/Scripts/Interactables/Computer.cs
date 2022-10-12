using UnityEngine;

public class Computer : Interactables
{
    [SerializeField]
    GameObject computerScreen, programScreen,
        calendarMaterialScreen, emailScreenMaterial;


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
                GameManager.GetManager().dialogueManager.SetDialogue("IOrdenador");
               
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

        if (!GameManager.GetManager().programmed && (int)GameManager.GetManager().dayController.dayState > 1)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("Atardecer", delegate
            {
                GameManager.GetManager().blockController.UnlockAll(DayController.DayTime.Noche);
                GameManager.GetManager().blockController.Unlock("Bed");
                GameManager.GetManager().blockController.Unlock("Window");
            });
        
        }
    }

    public void ComputerCalendar()
    {
        if (anyButtonScreenActive)
            return;
        anyButtonScreenActive = true;
        GameManager.GetManager().calendarController.ShowCalendar();
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(true);
        emailScreenMaterial.SetActive(false);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerProgram()
    {
        if (anyButtonScreenActive)
            return;
        GameManager.GetManager().dialogueManager.SetDialogue("PCProgramar");
        
        programMinigame.SetActive(true);
        anyButtonScreenActive = true;
        programScreen.SetActive(true);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(false);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerEmail()
    {
        if (anyButtonScreenActive)
            return;
        GameManager.GetManager().dialogueManager.SetDialogue("PCMirarMail");
        anyButtonScreenActive = true;
        GameManager.GetManager().emailController.ShowEmail(true);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(true);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }
    #endregion
}
