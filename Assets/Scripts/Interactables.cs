using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    //este  script estaria bien enfocarlo a las funciones GET que tiene iinteract (trasladrlas aqu� para mayor comodidad)
    // Start is called before the first frame update

    public Canvas OptionsCanvas;
    public virtual void ShowCanvas()
    {
        if (OptionsCanvas.GetComponent<Animator>() && GameManager.GetManager().m_CurrentStateGame == GameManager.StateGame.GamePlay )
        {
            OptionsCanvas.GetComponent<Animator>().SetTrigger("Canvas");
        }
    }
}
