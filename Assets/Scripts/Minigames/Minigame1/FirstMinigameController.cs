
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class FirstMinigameController : MonoBehaviour
{
    public List<SolutionPiece> m_AllSolutions = new List<SolutionPiece>();
    public List<PieceMG> m_AllPieces = new List<PieceMG>();
    private bool m_Solved = false;
    private bool m_AllCorrected;
    public bool m_started;

    void Start()
    {
        GameManager.GetManager().ProgramMinigame = this;
      
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && m_started)
        {
            GameManager.GetManager().StartThirdPersonCamera();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator GameFinished()
    {
        print("game completed");
        GameManager.GetManager().Autocontrol.AddAutoControl(14);
        m_Solved = true;
        m_started = false;
        yield return new WaitForSeconds(2f);
        
        GameManager.GetManager().CanvasManager.FinishMiniGame();
    }

    public void CheckSolutions()
    {
        if (m_Solved)
            return;

        for (int i = 0; i < m_AllSolutions.Count; i++)
        {
            if (!m_AllSolutions[i].m_Correct)
                m_AllCorrected = false;
            else
                m_AllCorrected = true;
        }
        
        if(m_AllCorrected)
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
