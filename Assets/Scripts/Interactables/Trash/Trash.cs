using System.Collections;
using UnityEngine;

public class Trash : GeneralActions
{
    public enum TrashType { CLOTHES, TRASH }
    public TrashType type = TrashType.TRASH;
    // 5 prendas de ropa
    public int maxTras = 5;
    public Transform target;
    private Vector3 initPos;
    private float grabbingSpeed = 10f;
    private bool grabbing;
    private bool onlySpeakOnce;

    private void Start()
    {
        initPos = transform.position;
        grabbing = false;
        onlySpeakOnce = true;
    }

    public override void EnterAction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Clothes PickUp", transform.position);
        GameManager.GetManager().gameStateController.ChangeGameState(2);

        StartCoroutine(Delay());
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
        GameManager.GetManager().dialogueManager.SetDialogue("IRecogerHabitacion");
        GameManager.GetManager().playerController.SetAnimation("Grab");
        if (onlySpeakOnce && GameManager.GetManager().dayController.GetDayNumber() == DayController.Day.two)
        {
            GameManager.GetManager().dialogueManager.SetDialogue("D2AccHigLimp_Basura");
            onlySpeakOnce = false;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        ///  grabbing = true;
        // transform.position = Vector3.MoveTowards(transform.position, target.position, grabbingSpeed * Time.deltaTime);
        transform.position = GameManager.GetManager().playerController.handBone.position;
        transform.SetParent(GameManager.GetManager().playerController.handBone);

        yield return new WaitForSeconds(1);

        if (type == TrashType.TRASH)
            GameManager.GetManager().trashInventory.AddTrash(this);
        else
            GameManager.GetManager().trashInventory.AddDirtyClothes(this);


        yield return new WaitForSeconds(0.75f);
        GameManager.GetManager().gameStateController.ChangeGameState(1);
        gameObject.SetActive(false);
    }

    //private void Update()
    //{
    //    if (grabbing)
    //    {
    //        // transform.position = Vector3.MoveTowards(transform.position, target.position, grabbingSpeed * Time.deltaTime);
    //        transform.position = GameManager.GetManager().playerController.handBone.position;
    //        transform.SetParent(GameManager.GetManager().playerController.handBone);

    //        if (Vector3.Distance(transform.position, target.position) < 0.1f)
    //        {
    //            if (type == TrashType.TRASH)
    //                GameManager.GetManager().trashInventory.AddTrash(this);
    //            else
    //                GameManager.GetManager().trashInventory.AddDirtyClothes(this);

    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void ResetInteractable()
    {
        transform.position = initPos;
        grabbing = false;
        done = false;
    }
}
