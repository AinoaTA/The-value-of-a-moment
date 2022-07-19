using UnityEngine;

public class SolutionPiece : MonoBehaviour
{
    public GameObject m_CorrectPiece;

    public bool m_Correct;
    private PieceMG currPiece;

    private void OnTriggerEnter(Collider other)
    {
        currPiece = other.GetComponent<PieceMG>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == m_CorrectPiece && !currPiece.dragging && !currPiece.correct)
        {
            m_Correct = true;
            currPiece.correct = m_Correct;
        }
    }
}
