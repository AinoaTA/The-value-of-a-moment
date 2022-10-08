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
        calendarMaterialScreen.SetActive(false);
        anyButtonScreenActive = false;
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }

    public void ComputerOFF()
    {
        GameManager.GetManager().playerController.TemporalExit();
        //GameManager.GetManager().playerController.SetAnimation("Walk");
        computerScreen.SetActive(false);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(false);
        anyButtonScreenActive = false;
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
        anyButtonScreenActive = true;
        GameManager.GetManager().emailController.ShowEmail(true);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        emailScreenMaterial.SetActive(true);
        GameManager.GetManager().playerController.playerAnimation.InterctAnim();
    }
    #endregion
}
