using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float levitationHeight = 0.5f;
    public float levitationSpeed = 1.0f;
    public float rotationSpeed = 90.0f;

    private Vector3 initialPosition;

    [SerializeField] private int pickedCoins = 0;

    [SerializeField] private AudioClip coinSound;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Levitación.
        float verticalOffset = Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        Vector3 newPosition = initialPosition + new Vector3(0, verticalOffset, 0);
        transform.position = newPosition;

        // Rotación continua.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter()
    {
        pickedCoins++;
        //AudioSource.PlayClipAtPoint(coinSound, transform.position, 1);
        gameObject.SetActive(false);
    }
}
