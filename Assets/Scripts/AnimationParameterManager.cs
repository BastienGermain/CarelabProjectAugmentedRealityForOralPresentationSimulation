using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class AnimationParameterManager : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void SetSleeping(int value)
    {
        animator.SetInteger("Sleeping", value);
    }

    public void SetTalking(int id)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Seated Idle"))
        {
            animator.SetInteger("Talking", id);
        }       
    }

}