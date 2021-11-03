using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Server;

public class HoloLensApp : MonoBehaviour
{
    MessageServer server;

    // Start is called before the first frame update
    void Start()
    {
        server = new MessageServer();
        Debug.Log("Initialized server");

        server.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        server.Dispose();
    }
}
