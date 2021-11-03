using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Text;

public class MessageClient : MonoBehaviour
{
    UdpClient client;

    // Start is called before the first frame update
    void Start()
    {
        client = new UdpClient("127.0.0.1", 9000);

        client.Connect("127.0.0.1", 10000);

    }

    // Update is called once per frame
    void Update()
    {
        Byte[] sendBytes = Encoding.ASCII.GetBytes("Hello from Unity!");
        client.Send(sendBytes, sendBytes.Length);
    }

    public void OnMessageRecieved()
    {

    }
}
