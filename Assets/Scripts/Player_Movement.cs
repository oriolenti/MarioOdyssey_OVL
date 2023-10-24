using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;

    private float gravity = 20f;
    private float jumpForce = 8f;
    private float coyoteTime = 1f;

    private CharacterController controller;


    public Transform target;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Calcular dirección XZ
        Vector3 direction = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        direction.Normalize();
       
        
        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;
        controller.Move(finalVelocity * Time.deltaTime);

        direction.y = -1f;

        //Mirada por dirección
        float grados = Vector3.SignedAngle(transform.forward, direction.normalized, Vector3.Cross(transform.forward, direction));

        transform.rotation = Quaternion.AngleAxis(grados, Vector3.Cross(transform.forward, direction));




        //Calcular gravedad
        if (controller.isGrounded)
        {
            finalVelocity.y = direction.y * gravity * Time.deltaTime;
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
        }
        controller.Move(finalVelocity * Time.deltaTime);

      
        //Saltar
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                finalVelocity.y = jumpForce;
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && coyoteTime >= 0f)
            {
                finalVelocity.y = jumpForce;
                coyoteTime = 0f;
            }
        }
    }
}
