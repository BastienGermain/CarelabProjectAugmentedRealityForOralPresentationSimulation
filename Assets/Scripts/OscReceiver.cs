using OscJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscReceiver : MonoBehaviour
{
    private OscServer server;

    void Start()
    {
        server = new OscServer(9100); // Port number

        server.MessageDispatcher.AddCallback(
            "/test", // OSC address
            (string address, OscDataHandle data) =>
            {
                Debug.Log(string.Format("({0}, {1}, {2})",
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1),
                    data.GetElementAsFloat(2)));
            }
        );
    }

    void OnDestroy()
    {
        server.Dispose();
    }

}




