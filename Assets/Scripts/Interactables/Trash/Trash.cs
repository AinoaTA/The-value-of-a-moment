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
    BoxCollider col;
    Transform parent;
    private void Start()
    {
        parent = transform.parent;
        initPos = transform.position;
        grabbing = false;
        onlySpeakOnce = true;
        col = GetComponent<BoxCollider>();
    }

    public override void EnterAction()
    {
        col.enabled = false;
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

    public override void ResetObject()
    {
        transform.SetParent(parent);
        col.enabled = true;
        transform.position = initPos;
        grabbing = false;
        done = false;
        base.ResetObject();
    }

    public void ResetInteractable()
    {
        //col.enabled = true;
        //transform.position = initPos;
        //grabbing = false;
        //done = false;
    }
}
