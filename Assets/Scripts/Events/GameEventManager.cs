using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _instance;
    private GameManager _gameManager;

    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _gameManager = GameManager.GetManager();
    }

    public static GameEventManager GetGameEventManager()
    {
        return _instance;
    }

    public void PlayEvent(GameEventTrigger trigger)
    {
        var currentDay = _gameManager.dayController.currentDay;
        var gameEvent = trigger.gameEvents.FirstOrDefault(i => i.eventDay == currentDay && !i.hasBeenTriggered);

        if (gameEvent != null) GameManager.GetManager().dialogueManager.SetDialogue(gameEvent.eventDialogue);
    }
}
