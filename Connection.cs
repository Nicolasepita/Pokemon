using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Pokemon
{
    public class Connection
    {
        private Socket SocketClient;
        private IPAddress IP;
        private int port;
        private bool isConnected = false;
        private byte[] sendingbuf;
        private byte[] msg;
        
        public Connection(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            this.port = port;
        }

        public IPAddress Ip => IP;

        public int Port => port;

        public bool IsConnected => isConnected;

        public void startClient()
        {
            SocketClient.BeginConnect(new IPEndPoint(IP, port), new AsyncCallback(connexionConnectCallback),
                SocketClient);
        }    
        
        private void connexionConnectCallback(IAsyncResult asyncResult)
        {
            SocketClient.EndConnect(asyncResult);
            isConnected = true;
        }

        public void sendMessage(string s)
        {
            sendingbuf = Encoding.UTF8.GetBytes(s);
            SocketClient.BeginSend(sendingbuf, 0, sendingbuf.Length, SocketFlags.None, new AsyncCallback(SendCallback),
                SocketClient);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            //int send = SocketClient.EndSend(asyncResult);
        }

        public string getNewMessages()
        {
            string msg = null;
            if (isReady())
            {
                this.msg = new byte[SocketClient.Available];
                SocketClient.BeginReceive(this.msg, 0, this.msg.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback),
                    SocketClient);
                msg = Encoding.UTF8.GetString(this.msg);
                OnReciveMessage(msg);
            }
            return msg;
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            //int read = SocketClient.EndReceive(asyncResult);
        }
        
        protected virtual void OnReciveMessage(string s)
        {
            //=)
        }
        
        public void Close()
        {
            SocketClient.Close();
            isConnected = false;
        }
        
        public bool isReady()
        {
            if (SocketClient != null)
            {
                return (SocketClient.Available != 0);
            }
            return false;
        }
    }
}