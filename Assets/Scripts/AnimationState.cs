using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : MonoBehaviour
{
    private Animator animator;
    private Vector3 previousPosition;
    public GAction currentAction; // The currently executing action
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        float distanceTraveled = (transform.position - previousPosition).magnitude;

        if (currentAction.running)
        {
            if (currentAction is GoPlantTree)
            {
                animator.SetBool("IsChopping", false);
                animator.SetBool("IsPlanting", true);
            }
            else if (currentAction is GoToForest)
            {
                if (distanceTraveled != 0)
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsChopping", false);
                    animator.SetBool("IsMoving", true);
                }
                else
                {
                    animator.SetBool("IsPlanting", false);
                    animator.SetBool("IsChopping", true);
                    animator.SetBool("IsMoving", false);
                }
            }
            else
            {
                animator.SetBool("IsPlanting", false);
                animator.SetBool("IsChopping", false);
            }
        }

        previousPosition = transform.position;
    }
}

