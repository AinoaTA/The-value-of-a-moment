using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum StateGame
    {
        Init = 0,   // Momento despertar-posponer
        GamePlay,   // Una vez despertado y moviendose por el nivel
        MiniGame    // Se ha iniciado un minigame
    }
    private StateGame currentStateGame;

    private void Awake()
    {
        GameManager.GetManager().gameStateController = this;
    }

    /// <summary>
    /// 0 - Init;
    /// 1 - GamePlay;
    /// 2 - MiniGame
    /// </summary>
    /// <param name="state"></param>
    public void ChangeGameState(int state)
    {
        currentStateGame = (StateGame)state;
        //StartCoroutine(Delay((StateGame)state));
    }
    /// <summary>
    /// Check current state mode. If it matches, return true.
    /// 
    /// 0 - Init;
    /// 1 - GamePlay;
    /// 2 - MiniGame
    /// </summary>
    /// <param name="stateID"></param>
    /// <returns></returns>
    public bool CheckGameState(int stateID) 
    {
        return currentStateGame == (StateGame)stateID;
    }
}
