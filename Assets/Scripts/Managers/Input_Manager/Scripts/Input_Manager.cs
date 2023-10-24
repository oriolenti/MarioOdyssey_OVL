using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed;

    private Vector2 LeftAxisValue = Vector2.zero;


    private void Awake()
    {
        //Compruebo existencia de instancias al input manager

        if(_INPUT_MANAGER != null && _INPUT_MANAGER != this)
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

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        
        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        LeftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude: " + LeftAxisValue.magnitude);
        Debug.Log("Normalize: " + LeftAxisValue.normalized);
    }

    public bool GetSouthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    public bool GetNorthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    public bool GetEastButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    public bool GetWestButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }


}
