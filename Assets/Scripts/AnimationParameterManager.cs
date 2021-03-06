﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class attatched to the avatars to handle their animations

public class AnimationParameterManager : MonoBehaviour
{
    private Animator animator;
    private PointsManager pointsManager;
    private float timer = 0.0f;
    private float waitTime = 1.0f;
    private float attentionVoiceLevel = 1.0f;
    private float attentionGazeLevel = 1.0f;
    private bool sleeper;    

    void Start()
    {
        animator = GetComponent<Animator>();
        pointsManager = GameObject.Find("PointsManager").GetComponent<PointsManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            if (sleeper && attentionVoiceLevel < 0.5f)
            {
                animator.SetInteger("Sleeping", 1);
                pointsManager.AddPoints(-2);
            }
            else if (sleeper)
            {
                animator.SetInteger("Sleeping", 0);
                pointsManager.AddPoints(1);
            }            

            if (!sleeper && attentionGazeLevel < 0.5f)
            {
                animator.SetInteger("Talking", 1);
                pointsManager.AddPoints(-2);
            }
            else if (!sleeper)
            {
                animator.SetInteger("Talking", 0);
                pointsManager.AddPoints(1);
            }

            attentionVoiceLevel -= 0.05f;
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

    public void SetSleeper(bool value)
    {
        sleeper = value;
    }
}