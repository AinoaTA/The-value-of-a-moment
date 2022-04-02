using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    public Vector3 offSet;
    private Vector3 offSetExit = new Vector3(0.5f,0,0.5f);

    [HideInInspector] public bool dragg;

    private void OnMouseDrag()
    {
           dragg = true;
            //no me preguntes por el offset, la acción sin offset no funciona basically.
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offSet);
        
       
    }

    private void OnMouseUp()
    {
        dragg = false;
    }

}
