using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedMinigame : MonoBehaviour
{
    private int ValueConfident=5; //
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            MinigameCompleted();
    }

    private void MinigameCompleted()
    {
        
        GameManager.GetManager().GetBed().BedDone();
        GameManager.GetManager().GetCanvasManager().DesctiveBedCanvas();
        GameManager.GetManager().GetAutoControl().AddAutoControl(ValueConfident);
    }
}
