using System.Collections;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum StateGame
    {
        Init = 0,   // Momento despertar-posponer
        GamePlay,   // Una vez despertado y moviendose por el nivel
        MiniGame    // Se ha iniciado un minigame
    }
     public StateGame m_CurrentStateGame;

    private void Awake()
    {
        GameManager.GetManager().gameStateController = this;
    }

    /// <summary>
    /// 0 - init;
    /// 1 - GamePlay;
    /// 2 - MiniGame
    /// </summary>
    /// <param name="state"></param>
    public void ChangeGameState(int state)
    {
        StartCoroutine(Delay((StateGame)state));
    }
    private IEnumerator Delay(StateGame state)
    {
        yield return new WaitForSecondsRealtime(1);
        m_CurrentStateGame = state;
    }
}
