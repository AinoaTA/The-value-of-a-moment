
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class FirstMinigameController : Interactables
{
    public List<SolutionPiece> m_AllSolutions = new List<SolutionPiece>();
    public List<PieceMG> m_AllPieces = new List<PieceMG>();
    private bool m_Solved = false;
    private bool checking;
    private bool m_AllCorrected;
    public bool m_started;
    public float m_Autocontrol=5;

    void Start()
    {
        GameManager.GetManager().ProgramMinigame = this;
    }

    private IEnumerator GameFinished()
    {
        CheckDoneTask();
        print("game completed");
        GameManager.GetManager().Autocontrol.AddAutoControl(m_Autocontrol);
        m_Solved = true;
        m_started = false;
        yield return new WaitForSeconds(2f);
        
    }

    public void QuitMiniGame()
    {
        GameManager.GetManager().CanvasManager.FinishMiniGame();
    }


    public void CheckSolutions()
    {
        if (m_Solved || checking)
            return;

        checking = true;
        for (int i = 0; i < m_AllSolutions.Count; i++)
        {
            if (!m_AllSolutions[i].m_Correct)
                m_AllCorrected = false;
            else
                m_AllCorrected = true;
        }
        checking = false;

        if (m_AllCorrected)
            StartCoroutine(GameFinished());
    }

    public void ResetAllGame()
    {
        for (int i = 0; i < m_AllPieces.Count; i++)
        {
            m_AllPieces[i].ResetPiece();
        }
        m_Solved = false;
        m_started = false;
    } 
    public bool GetSolved() { return m_Solved; }
}
