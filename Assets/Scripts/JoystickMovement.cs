using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    public Transform buttonCenter;
    float horzLeftInput;
    float vertLeftInput;
    public Transform playerCamera;
    public Rigidbody playerRigidbody;

    private float moveSpeed = 5f;

    private Vector3 camForward;
    private Vector3 right;
    private Vector3 theDirection;
    private Vector3 movement;

    private float horzRightInput;
    private float stickTurnSpeed = 180f;

    private void Update()
    {
        //horzLeftInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        //vertLeftInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        horzLeftInput = Input.GetAxis("Horizontal");
        vertLeftInput = Input.GetAxis("Vertical");


        //Movement based on Looking
        camForward = playerCamera.forward; //attached to player object 
        right = new Vector3(camForward.z, 0, -camForward.x);

        theDirection = horzLeftInput * right + vertLeftInput * camForward;
        movement = (theDirection * moveSpeed) * Time.deltaTime; 

        playerRigidbody.MovePosition(transform.position + movement);

        //Turning
        //horzRightInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
        //transform.Rotate(0, horzRightInput * stickTurnSpeed * Time.deltaTime, 0);
    }
}
               
 
