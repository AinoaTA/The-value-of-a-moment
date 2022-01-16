
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class FirstMinigameController : MonoBehaviour
{
    //minijuego 1 del ordenador (continuar juego).
    public List<SolutionPiece> m_AllSolutions=new List<SolutionPiece>();
    public List<PieceMG> m_AllPieces = new List<PieceMG>();
    //private bool[] CorrectsSolutions = { false, false, false, false };

    private bool m_Solved=false;

    private void Awake()
    {
        GameManager.GetManager().SetFirstMiniGame(this);
    }
    private void Update()
    {
        if (CheckSolutions() &&!m_Solved && Input.GetMouseButtonUp(0))
        {
            print("FINISHED");
            StartCoroutine(GameFinished());
        }
            
    }


    private IEnumerator GameFinished()
    {
        GameManager.GetManager().GetAutoControl().AddAutoControl(14);
        m_Solved = true;
        yield return new WaitForSeconds(2f);
        
        GameManager.GetManager().GetCanvasManager().FinishMiniGame();
    }

    private bool CheckSolutions()
    {
        for (int i = 0; i < m_AllSolutions.Count; i++)
        {
            if (!m_AllSolutions[i].m_Correct)
                return false;
        }
        return true;
    }
    public void ResetAllGame()
    {
        for (int i = 0; i < m_AllPieces.Count; i++)
        {
            m_AllPieces[i].ResetPiece();
        }
        m_Solved = false;
    } 
    public bool GetSolved() { return m_Solved; }
}
