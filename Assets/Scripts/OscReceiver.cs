using OscJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscReceiver : MonoBehaviour
{
    public GameObject armsRedIcon;
    public GameObject armsGreenIcon;
    public GameObject handsRedIcon;
    public GameObject handsGreenIcon;
    public GameObject shouldersRedIcon;
    public GameObject shouldersGreenIcon;
    private OscServer server;
    private float waitTime = 5.0f;
    private float timer = 0.0f;
    private bool isArmsCrossed = false;

    void Start()
    {
        server = new OscServer(9100); // Port number        
        
        server.MessageDispatcher.AddCallback(
            "/test", // OSC address
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
             
    }

    void Update()
    {
        // update timer
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            Debug.Log(waitTime + " seconds elapsed");
            Debug.Log("arms crossed : " + isArmsCrossed);

            if(isArmsCrossed)
            {
                armsRedIcon.SetActive(true);
            } else
            {
                armsRedIcon.SetActive(false);
            }

            // remove the recorded seconds
            timer = timer - waitTime;
        }
    }

    void OnDestroy()
    {
        server.Dispose();
    }
}




