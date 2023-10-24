using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    private void Awake()
    {

        float dotProduct = Vector3.Dot(transform.forward, transform.right);

        //Me falta sacar el coseno

        float radiansDotProduct = Mathf.Acos(dotProduct);

        //Paso radianes a grados

        float radiansToDegrees =  radiansDotProduct * Mathf.Rad2Deg;
        
        
        //

        //Angulo Positivo
        
        //float angulo = Vector3.SignedAngle(transform.forward, transform.right, transform.forward);
        
        //Angulo Negativo
        //float angulo = Vector3.SignedAngle(transform.right, transform.forward, transform.up);
        
        
        float degrees = Vector3.SignedAngle(transform.forward, transform.right, Vector3.Cross(transform.forward, transform.right));
        Vector3 cross = Vector3.Cross(transform.forward, transform.right); //(0,0,1) * (1,0,0) = (0,1,0)

        Debug.Log(cross);
    }
}
