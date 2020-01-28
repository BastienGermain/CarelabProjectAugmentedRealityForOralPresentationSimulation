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
    private float attentionVoiceLevel = 1.0f;
    private float attentionGazeLevel = 1.0f;

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

            if (attentionVoiceLevel < 0.5f)
            {
                animator.SetInteger("Sleeping", 1);
            }
            else
            {
                animator.SetInteger("Sleeping", 0);
            }

            attentionVoiceLevel -= 0.05f;

            if (attentionGazeLevel < 0.5f)
            {
                animator.SetInteger("Talking", 1);
            }
            else
            {
                animator.SetInteger("Talking", 0);
            }

            attentionGazeLevel -= 0.05f;

            timer = timer - waitTime;
        }        
    }

    public void IncreaseVoiceAttention()
    {
        attentionVoiceLevel = 1.0f;
    }

    public void IncreaseGazeAttention()
    {
        attentionGazeLevel = 1.0f;
    }

}