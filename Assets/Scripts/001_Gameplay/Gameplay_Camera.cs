using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private float cameraLerp; //12f

    private float rotationX;
    private float rotationY;

    public static Input_Manager _INPUT_MANAGER;
    private PlayerInputActions playerInputs;

    private void LateUpdate()
    {
        //Rotation depende de Mouse Input ahora
        rotationX += Input_Manager._INPUT_MANAGER.GetRightJoystick().y;
        rotationY += Input_Manager._INPUT_MANAGER.GetRightJoystick().x;


        rotationX = Mathf.Clamp(rotationX, -10f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0); 

        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }

        transform.position = finalPosition;

    }
}
