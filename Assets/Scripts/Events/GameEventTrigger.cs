using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEventTrigger : MonoBehaviour
{
    public List<GameEvent> gameEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventManager.GetGameEventManager().PlayEvent(this);
        }
    }
}

[System.Serializable]
public class GameEvent
{
    public string eventDialogue;
    public DayController.Day eventDay;
    public bool hasBeenTriggered = false;
}
