using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimation : MonoBehaviour {

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      
    }

    public void SetArmsCrossed(int value)
    {
        animator.SetInteger("Arms crossed", value);
    }
}
