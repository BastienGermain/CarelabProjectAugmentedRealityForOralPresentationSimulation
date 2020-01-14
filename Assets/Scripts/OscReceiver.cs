using OscJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscReceiver : MonoBehaviour
{
    private OscServer server;
    private GameObject[] avatars;
    private AvatarAnimation script;
    private int armsInt = 0;

    void Start()
    {
        server = new OscServer(9100); // Port number

        avatars = GameObject.FindGameObjectsWithTag("Avatar");

        script = avatars[0].GetComponent<AvatarAnimation>();

        if (avatars != null)
        {
            //Debug.Log("found avatar");

            server.MessageDispatcher.AddCallback(
                "/test", // OSC address
                (string address, OscDataHandle data) =>
                {
                    Debug.Log(string.Format("({0})",
                        data.GetElementAsInt(0)));
                    armsInt = data.GetElementAsInt(0);
                }
            );
        }        
    }

    void Update()
    {
        if (script)
        {
            script.SetArmsCrossed(armsInt);
        }
    }

    void OnDestroy()
    {
        server.Dispose();
    }

    void ModifyParameter()
    {
        
    }

}




