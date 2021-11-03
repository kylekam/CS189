using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using UnityEngine;

namespace Assets.Client
{
    class MessageClient : IDisposable
    {
        private UdpClient client;

        public MessageClient()
        {
            client = new UdpClient();
            Debug.Log("Initialized Unity UDP Client");
            client.Connect("127.0.0.1", 10000);
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void SendMessage(string message)
        {
            Byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            client.Send(messageBytes, messageBytes.Length);
        }

        
    }
}
