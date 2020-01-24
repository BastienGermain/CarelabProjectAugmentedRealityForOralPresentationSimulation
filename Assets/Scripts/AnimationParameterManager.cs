using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class AnimationParameterManager : MonoBehaviour
{
    private Animator animator;
    private float timer = 0.0f;
    private float waitTime = 1.0f;
    private float attentionLevel = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            //Debug.Log("attention " + attentionLevel);

            if (attentionLevel < 0.5f)
            {
                animator.SetInteger("Sleeping", 1);
            }
            else
            {
                animator.SetInteger("Sleeping", 0);
            }

            attentionLevel -= 0.05f;            
            timer = timer - waitTime;
        }        
    }

    public void IncreaseAttention()
    {
        attentionLevel = 1.0f;
    }

    public void SetTalking(int id)
    {
        animator.SetInteger("Talking", id);      
    }

}