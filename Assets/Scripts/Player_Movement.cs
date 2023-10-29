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
    private float currentSpeed = 0f;

    [SerializeField] private int maxJumps = 3;

    private float gravity = 20f;
    private float jumpForce = 8f;

    [SerializeField] private float coyoteTime = 1f;
    [SerializeField] private float coyoteJump = 2f;

    private bool isCrouching = false;

    public static Input_Manager _INPUT_MANAGER;
    private PlayerInputActions playerInputs;

    private Animator animator;
    private Animator_Mario animationController;
    private bool isRunning = false;
    private bool isJump1 = false;
    private bool isJump2 = false;
    private bool isJump3 = false;

    [SerializeField] private AudioClip lavaSound;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<Animator_Mario>();
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


        //Calcular Speed para Animator - No funciona al final se pasa directamente
        if (movementInput != Vector2.zero && finalVelocity != Vector3.zero)
        {
            currentSpeed += velocityXZ * Time.deltaTime;
            animator.SetBool("running", true);
        }
        else
        {
            currentSpeed -= velocityXZ * Time.deltaTime;
            animator.SetBool("running", false);
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);


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
                    animator.SetBool("jump1", true);
                    isJump1 = true;

                    maxJumps--;
                    finalVelocity.y = jumpForce;
                    coyoteJump = 1f;
                }
                else if (maxJumps == 2)
                {
                    animator.SetBool("jump2", true);
                    isJump2 = true;

                    maxJumps--;
                    finalVelocity.y = jumpForce + 5;
                    coyoteJump = 2f;
                }
                else if (maxJumps == 1)
                {
                    animator.SetBool("jump3", true);
                    isJump3 = true;

                    finalVelocity.y = jumpForce + 10;
                    maxJumps = 3;
                }
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;

                animator.SetBool("jump1", false);
                animator.SetBool("jump2", false);
                animator.SetBool("jump3", false);
                isJump1 = false;
                isJump2 = false;
                isJump3 = false;
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

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Lava"))
        {
            transform.localPosition = Vector3.zero;
            /*
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = lavaSound;
            audio.Play();
            */
        }
    }

    public float GetCurrentSpeed()
    {
        return this.currentSpeed;
    }

    public bool GetRunning()
    {
        return this.isRunning;
    }

    public bool GetJump1()
    {
        return this.isJump1;
    }
    public bool GetJump2()
    {
        return this.isJump2;
    }
    public bool GetJump3()
    {
        return this.isJump3;
    }
}