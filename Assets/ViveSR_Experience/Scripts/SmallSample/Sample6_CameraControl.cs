﻿using UnityEngine;

namespace Vive.Plugin.SR.Experience
{
    public class Sample6_CameraControl : MonoBehaviour
    {
        [SerializeField] ViveSR_Experience_CameraControl CameraControlScript;

        private void Awake()
        {
            ViveSR_Experience.instance.CheckHandStatus(() =>
            {
                PlayerHandUILaserPointer.CreateLaserPointer();
                ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad;
                ViveSR_Experience_ControllerDelegate.triggerDelegate += HandleTrigger;

                CameraControlScript.gameObject.SetActive(true);   
            });
        }

        void HandleTrigger(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:
                    ViveSR_Experience_ControllerDelegate.touchpadDelegate -= HandleTouchpad;
                    break;
                case ButtonStage.PressUp:
                    ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad;
                    break;
            }
        }

        void HandleTouchpad(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.Press:
                    CameraControlScript.ResetPanelPos();
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            CameraControlScript.Reset();
        }
    }
}