using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneBar : MonoBehaviour
{
    public Transform barPivot;
    private float autoconfidence;

    void Start()
    {
        autoconfidence = GameManager.GetManager().autocontrol.GetAutcontrolValue();
        Debug.Log(autoconfidence);
    }

    void Update()
    {
        
    }
}
