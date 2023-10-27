using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private float timeSinceJumpPressed = 0f;
    private float timeMouse = 0f;


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



            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        InputSystem.Update();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        this.timeSinceJumpPressed = 0;
    }

    public bool GetSouthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }

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
