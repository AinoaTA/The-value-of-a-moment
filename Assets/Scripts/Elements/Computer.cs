using UnityEngine.UI;
using UnityEngine;

public class Computer : Interactables
{
    [SerializeField]
    private GameObject computerScreen, programScreen,
        /*calendarScreen*/ calendarMaterialScreen;

    private bool anyButtonScreenActive;

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
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom In", transform.position);
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                ComputerON();
                GameManager.GetManager().canvasController.ComputerScreenIn();
                break;
        }
    }

    public override void ExitInteraction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Zoom Out", transform.position);
        GameManager.GetManager().canvasController.ComputerScreenOut();
        GameManager.GetManager().StartThirdPersonCamera();
        calendarMaterialScreen.SetActive(false);
        computerScreen.SetActive(false);
        if (anyButtonScreenActive)
        {
            GameManager.GetManager().programMinigame.QuitMiniGame();
            GameManager.GetManager().calendarController.BackCalendar();
        }
        base.ExitInteraction();
    }

    #region (des)-active gameObects
    public void ComputerON()
    {
        computerScreen.SetActive(true);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
        anyButtonScreenActive = false;
    }

    public void ComputerOFF()
    {
        computerScreen.SetActive(false);
        programScreen.SetActive(false);
        calendarMaterialScreen.SetActive(false);
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
    }

    public void ComputerProgram()
    {
        if (anyButtonScreenActive)
            return;
        anyButtonScreenActive = true;
        programScreen.SetActive(true);
        calendarMaterialScreen.SetActive(false);
    }
    #endregion
}
