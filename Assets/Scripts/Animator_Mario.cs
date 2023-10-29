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
        animator.SetBool("running", marioMovement.GetRunning());
        animator.SetBool("jump1", marioMovement.GetJump1());
        animator.SetBool("jump2", marioMovement.GetJump2());
        animator.SetBool("jump3", marioMovement.GetJump3());
    }

}
