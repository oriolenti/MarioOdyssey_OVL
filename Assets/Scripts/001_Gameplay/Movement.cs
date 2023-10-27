using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private GameObject player;

    [SerializeField] private Camera camera;

    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField] private float velocityXZ = 5f;
    [SerializeField] private int maxJumps = 3;

    private float gravity = 20f;
    private float jumpForce = 8f;

    [SerializeField] private float coyoteTime = 1f;
    [SerializeField] private float coyoteJump = 3f;

    private bool crouching = false;

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
        //Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 direction = new Vector3 (0, -1, 0);
        direction.Normalize();


        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;


        //Si está en el suelo...

        if (controller.isGrounded)
        {
            finalVelocity.y = -gravity * Time.deltaTime;
            coyoteJump = 1f;

            //Salto
            if (coyoteJump > 0f)
            {
                if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && maxJumps == 3)
                {
                    maxJumps--;
                    finalVelocity.y = jumpForce;
                }

                else if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && maxJumps == 2)
                {
                    maxJumps--;
                    finalVelocity.y = jumpForce + 5;
                }

                else if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && maxJumps == 1)
                {
                    finalVelocity.y = jumpForce + 10;
                    maxJumps = 3;
                }
            }
            else
            {
                maxJumps = 3;
            }

        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;

            coyoteJump -= Time.deltaTime;

            /*if (Input.GetKey(KeyCode.LeftControl)) {
                crouching = true;
            }*/

        }



        controller.Move(finalVelocity * Time.deltaTime);

        // Mirada por dirección
        /*if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
        */
    }
}