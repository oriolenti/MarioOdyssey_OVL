using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarManager : MonoBehaviour
{
    [SerializeField] private int starsToWin = 3;
    [SerializeField] private int pickedStars = 0;


    [SerializeField] private AudioClip starSound;
    [SerializeField] private AudioClip endSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            pickedStars++;
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(starSound, transform.position, 1);
            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        if (pickedStars >= starsToWin)
        {
            AudioSource.PlayClipAtPoint(endSound, transform.position, 1);
            SceneManager.LoadScene("EndingScene");
        }
    }
}
