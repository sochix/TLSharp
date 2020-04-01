using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TLSharp.Core.Network
{
    public delegate TcpClient TcpClientConnectionHandler(string address, int port);

    public class TcpTransport : IDisposable
    {
        private readonly TLClient tcpClient;
        private readonly BlockingCollection<TcpMessage> ReceievedMessage = new BlockingCollection<TcpMessage>();
        //private readonly NetworkStream stream;
        private int sendCounter = 0;

        public TcpTransport(string address, int port, TcpClientConnectionHandler handler = null)
        {
            //if (handler == null)
            //{
            var ipAddress = IPAddress.Parse(address);
            tcpClient = new TLClient(ipAddress, port, ReceiveMessage);
            tcpClient.ConnectAsync();
            //}
            //else
            //    tcpClient = handler(address, port);
        }

        public void ReceiveMessage(TcpMessage message)
        {
            ReceievedMessage.Add(message);
            //handle

        }

        public async Task Send(byte[] packet, CancellationToken token = default(CancellationToken))
        {
            if (!tcpClient.IsConnected)
                throw new InvalidOperationException("Client not connected to server.");

            var tcpMessage = new TcpMessage(sendCounter, packet);

            tcpClient.SendAsync(tcpMessage.Encode());
            sendCounter++;
        }

        public async Task<TcpMessage> Receive(CancellationToken token = default(CancellationToken))
        {
            return ReceievedMessage.Take();
        }



        public bool IsConnected
        {
            get
            {
                ; return this.tcpClient.IsConnected;
            }
        }


        public void Dispose()
        {
            if (tcpClient.IsConnected)
            {
                //stream.Close();
                tcpClient.DisconnectAsync();
                tcpClient.Dispose();
            }
        }
    }
}
