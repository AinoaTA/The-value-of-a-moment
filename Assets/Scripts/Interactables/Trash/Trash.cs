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
    private bool grabbing = false;

    private void Start()
    {
        initPos = transform.position;
    }

    public override void EnterAction()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Env/Clothes PickUp", transform.position);
        grabbing = true;
        GameManager.GetManager().actionObjectManager.LookingAnInteractable(null);
        GameManager.GetManager().dialogueManager.SetDialogue("IRecogerHabitacion");
    
    }

    private void Update()
    {
        if (grabbing)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, grabbingSpeed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, target.position) < 0.1f)
            {
                if (type == TrashType.TRASH)
                    GameManager.GetManager().trashInventory.AddTrash(this);
                else
                    GameManager.GetManager().trashInventory.AddDirtyClothes(this);

                gameObject.SetActive(false);
            }
        }
    }

    public void ResetInteractable()
    {
        transform.position = initPos;
        grabbing = false;
        done = false;
    }
}
