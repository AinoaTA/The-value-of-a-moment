using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMG : MonoBehaviour
{
    private bool m_Selected = false;
    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }
    private void Update()
    {
        if (m_Selected)
            Move();
    }

    private void Move()
    {
        transform.position = Input.mousePosition; 
    }

    public void Select()
    {
         m_Selected = !m_Selected;
    }

    public void ResetPiece()
    {
        m_Selected = false;
        transform.position = initialPos;
    }
}
