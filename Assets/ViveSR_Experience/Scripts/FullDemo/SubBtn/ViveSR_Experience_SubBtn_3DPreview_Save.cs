﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_SubBtn_3DPreview_Save : ViveSR_Experience_ISubBtn
    { 
        [SerializeField] ViveSR_Experience_Tutorial_InputHandler_3DPreview TutorialInputHandler_3DPreview;

        [SerializeField] _3DPreview_SubBtn SubBtnType;

        ViveSR_Experience_StaticMesh StaticMeshScript;

        int chairNum = 0;

        protected override void AwakeToDo()
        {
            ThisButtonTypeNum = (int)SubBtnType;             
        }

        protected override void StartToDo()
        {
            StaticMeshScript = ViveSR_Experience_Demo.instance.StaticMeshScript;
        }
        public override void ExecuteToDo()
        {
            ViveSR_Experience_Demo.instance.Rotator.RenderButtons(false);
            SubMenu.RenderSubBtns(false);

            ViveSR_Experience_ActionSequence actionSequence = ViveSR_Experience_ActionSequence.CreateActionSequence(gameObject);

            ViveSR_Experience_Demo.instance.StaticMeshScript.SetChairSegmentationConfig(true);

            chairNum = 0;

            actionSequence.AddAction(()=> StaticMeshScript.TestSegmentationResult(UpdatePercentage_Segmentation,actionSequence.ActionFinished));
            actionSequence.AddAction(()=>
            {
                List<SceneUnderstandingObjects.Element> SegResults = StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR);

                StaticMeshScript.GenerateHintLocators(SegResults);

                chairNum = StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR).Count;

                ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Segmentation].EnableButton(chairNum > 0);

                StaticMeshScript.SetSegmentation(false);
                StaticMeshScript.ExportModel(UpdatePercentage_Mesh, actionSequence.ActionFinished);
            }); 
            actionSequence.AddAction(()=>
            {
                ViveSR_Experience_Demo.instance.Rotator.RenderButtons(true);
                SubMenu.RenderSubBtns(true);

                isOn = false;

                //Disable the [Save] button.
                SubMenu.subBtnScripts[ThisButtonTypeNum].isOn = false;
                SubMenu.subBtnScripts[ThisButtonTypeNum].EnableButton(false);

                //Enable the [Scan] button.
                ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Scan].ForceExcute(false);
                ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Scan].EnableButton(true);

                ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onController,"", false);
    
                ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onHeadSet, "Mesh & Chair Data Saved!", true);

                //[Enable Mesh] is available.
                if (StaticMeshScript.CheckModelExist())
                {
                    ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton.EnableMesh_StaticMR].EnableButton(true);
                    ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton.EnableMesh_StaticVR].EnableButton(true);
                }

                StaticMeshScript.ClearHintLocators();
                actionSequence.ActionFinished();
            });
            actionSequence.StartSequence();
        }

        void UpdatePercentage_Mesh(int percentage)
        {
            ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onController, "Saving " + chairNum.ToString() + " Chair" + "\nSaving Mesh Data..." + percentage + "%", false);
        }

        void UpdatePercentage_Segmentation(int percentage)
        {
            ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onController, "Saving Chair Data..." + percentage.ToString() + "%", false);
        }


    }
}