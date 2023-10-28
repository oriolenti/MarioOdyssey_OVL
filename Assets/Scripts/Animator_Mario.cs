using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Mario : MonoBehaviour
{
    private Player_Movement marioMovement;
    private Animator animator;
    
    
    private void Awake()
    {
        marioMovement = GetComponent<Player_Movement>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetFloat("velocity", marioMovement.GetCurrentSpeed());
    }

}
