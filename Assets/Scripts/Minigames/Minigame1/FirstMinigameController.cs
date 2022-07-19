
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class FirstMinigameController : MonoBehaviour
{
    public List<SolutionPiece> AllSolutions = new List<SolutionPiece>();
    public List<PieceMG> AllPieces = new List<PieceMG>();
    private bool Solved = false;
    private bool checking;
    private bool AllCorrected;
    public bool started;
    public float Autocontrol=5;

    void Start()
    {
        GameManager.GetManager().ProgramMinigame = this;
    }

    private IEnumerator GameFinished()
    {
        GameManager.GetManager().dayNightCycle.TaskDone();
        //CheckDoneTask();
        GameManager.GetManager().Autocontrol.AddAutoControl(Autocontrol);
        Solved = true;
        started = false;
        yield return new WaitForSeconds(0.5f);
        GameManager.GetManager().CanvasManager.FinishMiniGame();
    }

    public void QuitMiniGame()
    {
        GameManager.GetManager().CanvasManager.FinishMiniGame();
    }


    public void CheckSolutions()
    {
        if (Solved || checking)
            return;

        checking = true;
        for (int i = 0; i < AllSolutions.Count; i++)
        {
            if (!AllSolutions[i].Correct)
                AllCorrected = false;
            else
                AllCorrected = true;
        }
        checking = false;

        if (AllCorrected)
            StartCoroutine(GameFinished());
    }

    public void ResetAllGame()
    {
        for (int i = 0; i < AllPieces.Count; i++)
        {
            AllPieces[i].ResetPiece();
        }
        Solved = false;
        started = false;
    } 
    public bool GetSolved() { return Solved; }
}
