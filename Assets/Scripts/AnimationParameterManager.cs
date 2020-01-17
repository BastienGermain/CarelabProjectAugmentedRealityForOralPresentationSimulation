using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}