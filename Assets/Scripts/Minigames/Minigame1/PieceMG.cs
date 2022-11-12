using UnityEngine;
using UnityEngine.UI;

public class PieceMG : MonoBehaviour
{
    [SerializeField] Vector3 initialPos;

    public bool correctWhole;
    public int id;
    public Image image;
    ButtonTrigger bTrigger;
    private void Start()
    {
        initialPos = transform.position;
        bTrigger = GetComponent<ButtonTrigger>();
    }
    public void ClickDown()
    {
        image.raycastTarget = false;
    }
    public void Drag()
    {
        if (correctWhole) return;
        Vector3 newPos = Input.mousePosition;
        newPos.z = 0;
        transform.position = newPos;
    }
    public void MouseUp()
    {
        if (GameManager.GetManager().programMinigame.currSolution != null
            && GameManager.GetManager().programMinigame.currSolution.id == id)
        {
            correctWhole = true;
            transform.position = GameManager.GetManager().programMinigame.currSolution.transform.position;
        }
        else if(!correctWhole)
        {
            correctWhole = false;
            transform.position = initialPos;
        }

        bTrigger.blocker = correctWhole;
        image.raycastTarget = true;
    }

    public void ResetPiece()
    {
        transform.position = initialPos;
        bTrigger.blocker = false;
        correctWhole = false;
    }
}
