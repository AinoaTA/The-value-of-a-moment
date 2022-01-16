using UnityEngine;

public class SolutionPiece : MonoBehaviour
{
    public GameObject m_CorrectPiece;

    public bool m_Correct;


    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == m_CorrectPiece)
            m_Correct = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == m_CorrectPiece)
            m_Correct = false;
    }
}
