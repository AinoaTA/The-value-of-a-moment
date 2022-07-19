using UnityEngine;

public class SolutionPiece : MonoBehaviour
{
    public GameObject CorrectPiece;

    public bool Correct;
    private PieceMG currPiece;

    private void OnTriggerEnter(Collider other)
    {
        currPiece = other.GetComponent<PieceMG>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == CorrectPiece && !currPiece.dragging && !currPiece.correct)
        {
            Correct = true;
            currPiece.correct = Correct;
        }
    }
}
