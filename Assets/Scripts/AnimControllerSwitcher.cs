using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControllerSwitcher : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController Idle;
    public RuntimeAnimatorController Moving;
    public RuntimeAnimatorController Running;
    public RuntimeAnimatorController Strafing;

    private PlayerMovement playerMovement;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        playerMovement = GetComponent<PlayerMovement>();
        animator.runtimeAnimatorController = Idle;
    }

    void Update()
    {
        Animate();
    }

    void Animate()
    {
        if (playerMovement.isMoving)
        {
            SwitchController(Moving);
        }
        else if (playerMovement.playRunAnim)
        {
            SwitchController(Running);
        }
        else if (playerMovement.isStrafing)
        {
            SwitchController(Strafing);
        }
        else if (playerMovement.isJumping)
        {
            //SwitchController(Jumping);
        }
        else
        {
            SwitchController(Idle);
        }
    }

    void SwitchController(RuntimeAnimatorController newController)
    {
        if (animator.runtimeAnimatorController != newController)
        {
            animator.runtimeAnimatorController = newController;
        }
    }
}
