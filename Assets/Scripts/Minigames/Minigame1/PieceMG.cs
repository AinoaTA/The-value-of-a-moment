using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMG : MonoBehaviour
{
    private bool m_Selected = false;
    private Vector3 initialPos;

    public bool dragging = false;
    public bool correct;
    private void Start()
    {
        initialPos = transform.position;
    }
    //este script me deja loca, no se como funciona jaja no sé q hice en su momento. !!!

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z =0;
        transform.position = newPos;
    }

    private void Update()
    {
        if (m_Selected)
            Move();
    }

    
    private void Move()
    {
        Vector3 newPos =Input.mousePosition;
        dragging = true;
        newPos.z = 0;
        transform.position = newPos;
    }

    public void Select()
    {
        m_Selected = !m_Selected;

        if (!m_Selected)
            dragging = false;
    }

    public void ResetPiece()
    {
        m_Selected = false;
        transform.position = initialPos;
    }
}
