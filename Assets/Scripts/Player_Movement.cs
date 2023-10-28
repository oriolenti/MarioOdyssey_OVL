using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private CharacterController controller;
    private GameObject player;

    [SerializeField] private Camera camera;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 lastPosition;

    [SerializeField] private float velocityXZ = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float currentSpeed = 0f;

    [SerializeField] private int maxJumps = 3;

    private float gravity = 20f;
    private float jumpForce = 8f;

    [SerializeField] private float coyoteTime = 1f;
    [SerializeField] private float coyoteJump = 2f;

    private bool isCrouching = false;

    public static Input_Manager _INPUT_MANAGER;
    private PlayerInputActions playerInputs;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<GameObject>();
    }

    private void Update()
    {
        // Calcular dirección XZ
        Vector2 movementInput = Input_Manager._INPUT_MANAGER.GetMovementInput();

        //Movimiento relativo a la cámara
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 direction = (cameraForward * movementInput.y + cameraRight * movementInput.x).normalized;

        direction.Normalize();


        // Movimiento y Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;


        //Si está en el suelo...
        if (controller.isGrounded)
        {
            //CoyoteJump timer start
            if (coyoteJump > 1f)
            {
                coyoteJump -= Time.deltaTime;
            }
            //If CoyoteJump ends resets maxJumps
            else if (coyoteJump <= 0f)
            {
                maxJumps = 3;
            }

            // Jump
            if (Input_Manager._INPUT_MANAGER.GetJumpButtonPressed())
            {
                if (maxJumps == 3)
                {
                    maxJumps--;
                    finalVelocity.y = jumpForce;
                    coyoteJump = 1f;
                }
                else if (maxJumps == 2)
                {
                    maxJumps--;
                    finalVelocity.y = jumpForce + 5;
                    coyoteJump = 2f;
                }
                else if (maxJumps == 1)
                {
                    finalVelocity.y = jumpForce + 10;
                    maxJumps = 3;
                }
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
            }

            // Crouching
            /*
            if (Input_Manager._INPUT_MANAGER.GetCrouchButtonPressed())
            {
                Debug.Log("Croucheando");
                isCrouching = true;
            }
            else if (Input_Manager._INPUT_MANAGER.GetCrouchButtonReleased())
            {
                Debug.Log("Dejó de crouchear");
                isCrouching = false;
            }
            */
        }
        else
        {
            finalVelocity.y -= gravity * Time.deltaTime;

            if (coyoteJump >= 0f)
            {
                coyoteJump -= Time.deltaTime;
            }
        }

        controller.Move(finalVelocity * Time.deltaTime);

        // Mirada por dirección
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }

        //Calcular Speed para Animator
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        //currentSpeed = horizontalVelocity.magnitude;

        currentSpeed += velocityXZ * Time.deltaTime;

    }

    public float GetCurrentSpeed()
    {
        return this.currentSpeed;
    }
}