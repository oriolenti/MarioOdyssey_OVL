using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private float timeSinceJumpPressed = 0f;
    private float timeSinceCrouchButtonPressed = 0f;


    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 mouseAxisValue = Vector2.zero;


    private PlayerInputActions playerInputs;

    private void Awake()
    {
        //Compruebo existencia de instancias al input manager

        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Genero instancia y activo character scheme
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();

            //Delegates
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Camera.performed += MouseAxisUpdate;

            playerInputs.Character.Crouch.performed += CrouchButtonPressed;
            playerInputs.Character.Crouch.canceled += CrouchButtonReleased;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        timeSinceCrouchButtonPressed += Time.deltaTime;

        InputSystem.Update();
    }

    //Movement
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }
    public Vector2 GetMovementInput()
    {
        return this.leftAxisValue;
    }

    //Jump
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        this.timeSinceJumpPressed = 0f;
    }

    public bool GetJumpButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }


    //Crouching
    private void CrouchButtonPressed(InputAction.CallbackContext context)
    {
        this.timeSinceCrouchButtonPressed = 0f;
    }

    public bool GetCrouchButtonPressed()
    {
        return this.timeSinceCrouchButtonPressed == 0f;
    }

    private void CrouchButtonReleased(InputAction.CallbackContext context)
    {
        this.timeSinceCrouchButtonPressed = 0.1f;
    }

    public bool GetCrouchButtonReleased()
    {
        return this.timeSinceCrouchButtonPressed == 0f;
    }

    //Camera - Mouse
    private void MouseAxisUpdate(InputAction.CallbackContext context)
    {
        mouseAxisValue = context.ReadValue<Vector2>();
        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    public Vector2 GetRightJoystick()
    {
        return this.mouseAxisValue;
    }
}
