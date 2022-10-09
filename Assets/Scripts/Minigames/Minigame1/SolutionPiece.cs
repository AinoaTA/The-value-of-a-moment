using UnityEngine;

public class SolutionPiece : MonoBehaviour
{
    public int id;

    public void Set() 
    {
        print("in");
        GameManager.GetManager().programMinigame.SetCurrSolution(this);
    }

    public void UnSet()
    {
        print("out");
        GameManager.GetManager().programMinigame.SetCurrSolution(null);
    }
}
