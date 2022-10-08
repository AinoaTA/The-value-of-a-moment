using UnityEngine.UI;
using UnityEngine;

public class PieceMG : MonoBehaviour
{
    private Vector3 initialPos;

    public bool correctWhole;
    public int id;
    public Image image;
    private void Start()
    {
        initialPos = transform.position;
    }
    public void ClickDown()
    {
        image.raycastTarget = false;
    }
    public void Drag()
    {
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
        else
        {
            correctWhole = false;
            transform.position = initialPos;
        }
        image.raycastTarget = true;
    }

    public void ResetPiece()
    {
        transform.position = initialPos;
    }
}
