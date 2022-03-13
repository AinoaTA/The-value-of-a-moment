using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IntfInteract 
{
    public void Interaction();

    public string NameAction();

    public bool GetDone();

    public string[] GetPhrases();

    public float GetDistance();
}
