using System;
using UnityEngine;

public class Look : MonoBehaviour{
#pragma warning disable 649;
  [SerializeField]  private float sensitivity = 0.5f;
  [SerializeField] private Transform playerCamera;
    private float lookX, lookY, xRotation = 0f;

    private void Update(){
        transform.Rotate(Vector3.up, lookX * Time.deltaTime);
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 moveInput){
        lookX = moveInput.x * sensitivity * 150;
        lookY = moveInput.y * sensitivity;
    }
}