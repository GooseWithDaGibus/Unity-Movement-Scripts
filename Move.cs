﻿using UnityEngine;

public class Move : MonoBehaviour{
#pragma warning disable 649;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5, gravity = -9.81f, jumpHeight = 3.5f, slopeForce, slopeForceRayLength;
    [SerializeField] private LayerMask groundMask;
    private Vector2 horizontalInput;
    Vector3 verticalVelocity = Vector3.zero;
    private bool isGrounded, jumped;
   // public Transform groundCheck;

    public void ReceiveInput(Vector2 horizontalInput){
        this.horizontalInput = horizontalInput;
    }

    private void Update(){
        isGrounded = Physics.CheckSphere(transform.position, 1.1f, groundMask);
        
        if (isGrounded){
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (jumped){
            if (isGrounded){
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }

            jumped = false;
        }

        if ((horizontalInput.x != 0 || horizontalInput.y != 0) && OnSlope() && !jumped){
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }
        
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void OnJumpedPressed(){
        jumped = true;
    }

    private bool OnSlope(){
        if (jumped){
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength)){
            if (hit.normal != Vector3.up){
                return true;
            }
        }

        return false;
    }
}