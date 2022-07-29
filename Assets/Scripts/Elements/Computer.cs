using UnityEngine.UI;
using UnityEngine;

public class Computer : Interactables
{
    [SerializeField]
    private GameObject computerScreen, programScreen,
        calendarScreen, calendarMaterialScreen;

    public override void Interaction(int options)
    {
        base.Interaction(options);
        switch (options)
        {
            case 1:
                GameManager.GetManager().gameStateController.ChangeGameState(2);
                ComputerON();
                GameManager.GetManager().canvasController.ComputerScreenIn();
                break;
        }
    }

    public override void ExitInteraction()
    {
        GameManager.GetManager().canvasController.ComputerScreenOut();
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitInteraction();
    }

    #region (des)-active gameObects
    public void ComputerON()
    {
        computerScreen.SetActive(true);
        programScreen.SetActive(false);
        calendarScreen.SetActive(false);
    }

    public void ComputerOFF()
    {
        computerScreen.SetActive(false);
        programScreen.SetActive(false);
        calendarScreen.SetActive(false);

    }

    public void ComputerCalendar()
    {
        programScreen.SetActive(false);
        calendarScreen.SetActive(true);
        calendarMaterialScreen.SetActive(true);
    }

    public void ComputerProgram()
    {
        programScreen.SetActive(true);
        calendarScreen.SetActive(false); 
        calendarMaterialScreen.SetActive(false);
    }
    #endregion
}
