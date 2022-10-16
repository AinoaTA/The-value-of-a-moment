using System.Collections;
using TMPro;
using UnityEngine;

public class Shower : GeneralActions
{
    private static FMOD.Studio.EventInstance ShowerSFX;
    private float lowAutoConfidenceLimit = 50f;
    #region Extra Actions
    public DoSomething[] moreOptions;
    [System.Serializable]
    public struct DoSomething
    {
        public string name;
        public string animationName;
        public Interactables interaction;
        public string canvasText;
        //dialogues xdxd
    }

    bool duchado;

    private Vector3 positionOnEnter;

    public Animator canvas;
    public TMP_Text[] texts;
    public GameObject showerPos;

    void StartExtraInteraction(int id)
    {
        if (GameManager.GetManager().interactableManager.currInteractable == null)
        {
            GameManager.GetManager().interactableManager.LookingAnInteractable(moreOptions[id].interaction);
            moreOptions[id].interaction.ExtraInteraction();
        }
    }

    #endregion
    public override void EnterAction()
    {
        base.EnterAction();
        ShowerSFX.start();
        ShowerSFX.setParameterByName("ShowerOnOff", 0f);
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        positionOnEnter = GameManager.GetManager().playerController.GetPlayerPos();
        GameManager.GetManager().playerController.SetPlayerPos(showerPos.transform.position);
        duchado = true;
        if (GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("D2AccHigLimp_Ducha");
            GameManager.GetManager().IncrementInteractableCount();
        }
        if (GameManager.GetManager().autocontrol.m_currentValue < lowAutoConfidenceLimit)
        {
            //StartCoroutine(ShowOtherOptions());
        }
    }

    public override void ExitAction()
    {
        InteractableBlocked = true;
        ShowerSFX.setParameterByName("ShowerOnOff", 1f);
        ShowerSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.GetManager().dayController.TaskDone();
        Debug.Log("IF pendiente de revisar......");
        //if (GameManager.GetManager().interactableManager.currInteractable != null)
        //    GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        // canvas.SetBool("Showing", false);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().playerController.ResetPlayerPos(positionOnEnter);
        base.ExitAction();

        string ducha;
        if (!duchado) ducha = "DuchaNo";
        else ducha = "DuchaSi";

        GameManager.GetManager().dialogueManager.SetDialogue(ducha, delegate
        {
            StartCoroutine(Delay());
        });
    }
    IEnumerator Delay()
    {
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                canvas.gameObject.SetActive(false);
                yield return new WaitForSeconds(1);
                GameManager.GetManager().dialogueManager.SetDialogue("TutorialAgenda", delegate
                {
                    GameManager.GetManager().dayController.ChangeDay(1);
                    GameManager.GetManager().blockController.UnlockAll(DayController.DayTime.MedioDia);
                });

                break;
            case DayController.Day.two:
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        ShowerSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameplay");
        // canvas.SetBool("Showing", true);
    }

    public void ShowerOnOff (float ShowerStat)
    {
        ShowerSFX.setParameterByName("ShowerOnOff", ShowerStat);
    }

    IEnumerator ShowOtherOptions()
    {
        yield return null;
        //for (int i = 0; i < texts.Length; i++)
        //    texts[i].text = moreOptions[i].canvasText;

        //yield return new WaitForSeconds(1f);
        //canvas.gameObject.SetActive(true);
        //canvas.SetBool("Showing", true);
    }

    public override void DoInteraction(int id)
    {
        //if (id == 0) duchado = true;
        //StartExtraInteraction(id);
    }
    public override void ResetObject()
    {
        duchado = false;
        base.ResetObject();
    }
}
