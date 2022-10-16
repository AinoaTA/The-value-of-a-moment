using System.Collections;
using TMPro;
using UnityEngine;

public class Sit : GeneralActions
{
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

    public Animator canvas;
    public TMP_Text[] texts;

    void StartExtraInteraction(int id)
    {
        //if (GameManager.GetManager().interactableManager.currInteractable == null)
        //{
        //    GameManager.GetManager().interactableManager.LookingAnInteractable(moreOptions[id].interaction);
        //    moreOptions[id].interaction.ExtraInteraction();
        //}
    }

    #endregion
    public override void EnterAction()
    {
        canvas.gameObject.SetActive(true);
        base.EnterAction();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Elle/Sit", transform.position);
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        GameManager.GetManager().playerController.SetAnimation("Sit");
        switch (GameManager.GetManager().dayController.GetDayNumber())
        {
            case DayController.Day.one:
                break;
            case DayController.Day.two:
                GameManager.GetManager().dialogueManager.SetDialogue("D2AccDescRelax_Sofa");
                GameManager.GetManager().IncrementInteractableCount();
                break;
            case DayController.Day.three:
                break;
            case DayController.Day.fourth:
                break;
        }
        //  StartCoroutine(ShowOtherOptions());
    }

    public override void ExitAction()
    {
        //if (GameManager.GetManager().interactableManager.currInteractable != null)
        //    GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        // canvas.SetBool("Showing", false);
        GameManager.GetManager().playerController.SetAnimation("Stand");
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitAction();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Elle/GetInBed", transform.position);
    }

    private void Start()
    {
        //canvas.SetBool("Showing", true);   
    }

    IEnumerator ShowOtherOptions()
    {
        yield return null;
        //for (int i = 0; i < texts.Length; i++)
        //    texts[i].text = moreOptions[i].canvasText;

        //yield return new WaitForSeconds(1f);

        //canvas.SetBool("Showing", true);
    }

    public override void DoInteraction(int id)
    {
        //Debug.Log("Parece funcionar....");
        //StartExtraInteraction(id);
    }
}
