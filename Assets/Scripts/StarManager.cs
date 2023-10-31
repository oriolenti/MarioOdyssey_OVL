using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private int starsToWin = 10;
    [SerializeField] private int pickedStars = 0;
    [SerializeField] private int pickedCoins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            pickedStars++;
            other.gameObject.SetActive(false);
            CheckWinCondition();
        }
        else if (other.CompareTag("Coin"))
        {
            pickedStars++;
            other.gameObject.SetActive(false);
        }
    }

    private void CheckWinCondition()
    {
        if (pickedStars >= starsToWin)
        {
            //SceneManager      
        }
    }
}
