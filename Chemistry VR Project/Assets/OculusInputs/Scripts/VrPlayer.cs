using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayer : MonoBehaviour
{
    public Transform cameraController;
    CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        charController.height = cameraController.position.y;
    }

    /*
    public string horizontalInputName;
    public string verticalInputName;
    public bool snapTurn; // to be implemented
    public int snapAngle; // to be implemented

    public float movementSpeed = 2.0f;
    public float gravity = 20.0f;

    private CharacterController charController;
    private Vector3 moveDirection;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        horizontalInputName = "Left Horizontal";
        verticalInputName = "Left Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        // movement for when Player is grounded
        if (charController.isGrounded)
        {
            // getting movement values
            moveDirection = new Vector3(Input.GetAxis(horizontalInputName), 0, -Input.GetAxis(verticalInputName));
            Debug.Log(Input.GetAxis(verticalInputName));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
        }

        moveDirection.y -= (gravity * Time.deltaTime);

        charController.Move(moveDirection * Time.deltaTime);
    }
    */
}
