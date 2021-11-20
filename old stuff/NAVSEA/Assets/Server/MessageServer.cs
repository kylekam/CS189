using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using Assets.Client;
using UnityEngine;

namespace Assets.Server
{
    class MessageServer : IDisposable
    {
        private UdpClient server;
        private MessageClient client;

        public MessageServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);

            server = new UdpClient(endPoint);
            Debug.Log("UDP server binded on " + endPoint.ToString());

            client = new MessageClient();
        }

        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Listen));
            t.Start();
            Debug.Log("Started Listening Thread");
        }

        private void Listen()
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

            server.BeginReceive(new AsyncCallback(OnReceiveMessage), clientEndPoint);
            Debug.Log("Listening");
        }

        private void OnReceiveMessage(IAsyncResult result)
        {
            Debug.Log("Received packet");
            IPEndPoint endPoint = (IPEndPoint)result.AsyncState;

            Byte[] data = server.EndReceive(result, ref endPoint);
            string message = Encoding.ASCII.GetString(data);

            if (message == "disconnect")
            {
                return;
            }
            else if (message == "Hello from Python Client!")
            {
                Debug.Log("Received Message: " + message);
                client.SendMessage("Hello from Unity!");
                Debug.Log("Sent Hello Message Back");
            }
            else
            {
                Debug.Log("Received Message: " + message);
            }

            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            server.BeginReceive(new AsyncCallback(OnReceiveMessage), clientEndPoint);
            Debug.Log("Listening");
        }

        public void Dispose()
        {
            server.Dispose();
            client.Dispose();
        }
    }
}