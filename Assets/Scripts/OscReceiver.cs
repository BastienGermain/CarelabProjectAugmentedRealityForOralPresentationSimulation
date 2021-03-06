﻿using OscJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to receive OSC data from the BehaviourTracker in the Detection part

public class OscReceiver : MonoBehaviour
{
    public GameObject armsRedIcon;
    public GameObject armsGreenIcon;
    public GameObject handsRedIcon;
    public GameObject handsGreenIcon;
    public GameObject shouldersRedIcon;
    public GameObject shouldersGreenIcon;
    private OscServer server;
    private PointsManager manager;
    private float waitTime = 5.0f;
    private float timer = 0.0f;
    private bool isArmsCrossed = false;
    private bool isHandsInside = true;
    private bool isShouldersAligned = true;

    void Start()
    {
        manager = GameObject.Find("PointsManager").GetComponent<PointsManager>();

        server = new OscServer(9100); // Port number (needs to be identical to the one in BehaviourTracker in the Detection part)       
        
        server.MessageDispatcher.AddCallback(
            "/arms", // OSC address
            (string address, OscDataHandle data) =>
            {
                //Debug.Log(string.Format("({0})", data.GetElementAsInt(0)));
                if (data.GetElementAsInt(0) == 1)
                {
                    isArmsCrossed = true;
                }
                else
                {                 
                    isArmsCrossed = false;
                }
            }
        );

        server.MessageDispatcher.AddCallback(
            "/hands", // OSC address
            (string address, OscDataHandle data) =>
            {
                if (data.GetElementAsInt(0) == 1)
                {
                    isHandsInside = true;
                }
                else
                {
                    isHandsInside = false;
                }
            }
        );

        server.MessageDispatcher.AddCallback(
            "/shoulders", // OSC address
            (string address, OscDataHandle data) =>
            {
                if (data.GetElementAsInt(0) == 1)
                {
                    isShouldersAligned = true;
                }
                else
                {
                    isShouldersAligned = false;
                }
            }
        );

    }

    void Update()
    {
        // update timer
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            //Debug.Log(waitTime + " seconds elapsed");
            //Debug.Log("arms crossed : " + isArmsCrossed);

            if (isArmsCrossed)
            {
                armsRedIcon.SetActive(true);
                armsGreenIcon.SetActive(false);
                manager.AddPoints(-20);
            }
            else if (armsRedIcon.activeSelf)
            {
                armsRedIcon.SetActive(false);
                armsGreenIcon.SetActive(true);
                manager.AddPoints(30);
            } 
            else if (armsGreenIcon.activeSelf)
            {
                armsGreenIcon.SetActive(false);
            }
            
            if (!isHandsInside && !isArmsCrossed)
            {
                handsRedIcon.SetActive(true);
                handsGreenIcon.SetActive(false);
                manager.AddPoints(-20);
            }
            else if (handsRedIcon.activeSelf)
            {
                handsRedIcon.SetActive(false);
                handsGreenIcon.SetActive(true);
                manager.AddPoints(30);
            }
            else if (handsGreenIcon.activeSelf)
            {
                handsGreenIcon.SetActive(false);
            }
            
            if (!isShouldersAligned && isHandsInside && !isArmsCrossed)
            {
                shouldersRedIcon.SetActive(true);
                shouldersGreenIcon.SetActive(false);
                manager.AddPoints(-20);
            }
            else if (shouldersRedIcon.activeSelf)
            {
                shouldersRedIcon.SetActive(false);
                shouldersGreenIcon.SetActive(true);
                manager.AddPoints(30);
            }
            else if (shouldersGreenIcon.activeSelf)
            {
                shouldersGreenIcon.SetActive(false);
            }

            // remove the recorded seconds
            timer = timer - waitTime;
        }
    }

    void OnDestroy()
    {
        if(server != null)
            server.Dispose();
    }
}




