using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float movementSpeed = 2f;
    private bool busy;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!busy) Move();
    }

    private void Move()
    {
        Vector3 keyInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move((transform.TransformDirection(keyInput) * movementSpeed) * Time.deltaTime);
    }
}
