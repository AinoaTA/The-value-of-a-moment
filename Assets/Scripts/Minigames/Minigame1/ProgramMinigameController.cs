
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class ProgramMinigameController : MonoBehaviour
{
    public List<SolutionPiece> m_AllSolutions = new List<SolutionPiece>();
    public List<PieceMG> m_AllPieces = new List<PieceMG>();
    private bool solved = false;
    private bool checking;
    private bool m_AllCorrected;
    private bool gameInitialized;
    public float m_Autocontrol=5;

    private void Start()
    {
        GameManager.GetManager().programMinigame = this;
    }
    private IEnumerator GameFinished()
    {
        GameManager.GetManager().dayNightCycle.TaskDone();
        //CheckDoneTask();
        GameManager.GetManager().autocontrol.AddAutoControl(m_Autocontrol);
        gameInitialized = false;
        solved = true;
       
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().computer.ComputerON();
    }

    public void QuitMiniGame()
    {
        solved = false;
        gameInitialized = false;
        GameManager.GetManager().computer.ComputerON();
    }


    public void CheckSolutions()
    {
        if (solved || checking)
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
        solved = false;
        gameInitialized = false;
    } 
    public bool GetSolved() { return solved; }
}
