using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMG : MonoBehaviour
{
    private bool m_Selected = false;
    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }

    private void OnMouseDrag()
    {

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(newPos);
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
        //transform.position = Input.mousePosition;

        Vector3 newPos =Input.mousePosition;
        print(newPos);
        newPos.z = 0;
        transform.position = newPos;
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
