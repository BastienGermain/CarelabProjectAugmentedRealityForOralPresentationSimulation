﻿/*
 * Detect when the headset and an effect ball collide to switch shaders.
 */

using UnityEngine;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_EffectBallCollision : MonoBehaviour
    {
        [SerializeField] ViveSR_Experience_Effects EffectsScript;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "playerHeadCollision" || other.name == "HeadCollision")
            {
                //Assign the choosen shader.
                EffectsScript.ChangeShader(EffectsScript.CurrentEffectNumber);
                
                //Hide the current effect ball.
                gameObject.SetActive(false);
            }
        }
    }
}
