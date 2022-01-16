
using UnityEngine;

public class MinigameCode : MonoBehaviour
{
    //minijuego 1 del ordenador (continuar juego).
    private Camera cam;
    private bool m_Completed;
    public LayerMask m_LayerMask;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Ray l_Ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0)&& !m_Completed)
        {

            if (Physics.Raycast(l_Ray, out RaycastHit l_Hit, m_LayerMask))
            {
                //l_Hit.transform.position = cam.ScreenPointToRay(Input.mousePosition);

               
            }
        }
    }
}
