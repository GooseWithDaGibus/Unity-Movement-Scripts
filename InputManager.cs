
using UnityEngine;

public class InputManager : MonoBehaviour{
#pragma warning disable 649;
    
    [SerializeField] private Move movement;
    [SerializeField] private Look camera;
    private PlayerControls controls;
    private Vector2 leftStick, rightStick;

    private void Awake(){
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => leftStick = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => leftStick = Vector2.zero;
        controls.Gameplay.Camera.performed += ctx => rightStick = ctx.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += ctx => rightStick = Vector2.zero;
        controls.Gameplay.Jump.performed += _ => Jumped();
    }
    
    void Update(){
        movement.ReceiveInput(leftStick);
        camera.ReceiveInput(rightStick);
    }

    private void OnEnable(){
        controls.Enable();
    }

    private void OnDisable(){
        controls.Disable();
    }

    private void Jumped(){
        movement.OnJumpedPressed();
    }
}