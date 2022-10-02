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

        StartCoroutine(ShowOtherOptions());
    }

    public override void ExitAction()
    {
        if (GameManager.GetManager().interactableManager.currInteractable != null)
            GameManager.GetManager().interactableManager.currInteractable.EndExtraInteraction();
        GameManager.GetManager().interactableManager.LookingAnInteractable(null);
        canvas.SetBool("Showing", false);
        GameManager.GetManager().StartThirdPersonCamera();
        base.ExitAction();
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

        canvas.SetBool("Showing", true);
    }

    public override void DoInteraction(int id)
    {
        Debug.Log("Parece funcionar....");
        StartExtraInteraction(id);
    }
}
