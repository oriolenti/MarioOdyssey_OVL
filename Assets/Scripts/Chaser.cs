using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    //1 - Posiciones

    [SerializeField] GameObject destiny;
    [SerializeField] float speed = 1f;

    Vector3 myPosition;

    private void Update()
    {
        //MOVIMIENTO

        //1 - Consigo mi posición
        myPosition = transform.position;

        //2 - Consigo distancia con dirección
        Vector3 distanceDirection = destiny.transform.localPosition - myPosition;

        //3 - Normalizo y con ello obtengo el vector director, tanto si seteamos como geteamos, es lo mismo
         //SET
        Vector3 direction = distanceDirection.normalized;
         //GET
        //distanceDirection.Normalize();

        //4 - Me desplazo hacia una dirección, a una velocidad constante, teniendo en cuenta el incremento de tiempo
        transform.position += direction * speed * Time.deltaTime;


        //MIRADA PERSONAJE

        //1 - Posiciones 
        //2

        float grados = Vector3.SignedAngle(transform.forward, direction.normalized, Vector3.Cross(transform.forward, direction));

        transform.rotation = Quaternion.AngleAxis(grados, Vector3.Cross(transform.forward, direction));



    }
}
