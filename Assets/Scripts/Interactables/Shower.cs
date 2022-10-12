using System.Collections;
using TMPro;
using UnityEngine;

public class Shower : GeneralActions
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

    bool duchado;

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
        GameManager.GetManager().gameStateController.ChangeGameState(3);
        GameManager.GetManager().cameraController.StartInteractCam(nameAction);
        GameManager.GetManager().playerController.SetPlayerPos(showerPos.transform.position);

        StartCoroutine(ShowOtherOptions());
    }

    public override void ExitAction()
    {
        InteractableBlocked = true;
        Debug.Log("IF pendiente de revisar......");
        if (GameManager.GetManager().interactableManager.currInteractable != null)
            GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        canvas.SetBool("Showing", false);
        GameManager.GetManager().StartThirdPersonCamera();
        GameManager.GetManager().playerController.ResetPlayerPos();
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
                   
                    GameManager.GetManager().dayController.NextStateDay();
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
        canvas.SetBool("Showing", true);
    }

    IEnumerator ShowOtherOptions()
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].text = moreOptions[i].canvasText;

        yield return new WaitForSeconds(1f);
        canvas.gameObject.SetActive(true);
        canvas.SetBool("Showing", true);
    }

    public override void DoInteraction(int id)
    {
        if (id == 0) duchado = true;
        StartExtraInteraction(id);
    }
}
