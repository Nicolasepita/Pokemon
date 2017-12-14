using System;
using System.Net.Sockets;
using System.Text;

namespace Pokemon
{
    public class Clientsocket
    {
        private string name = "name not define";
        private Socket SocketClient;
        private bool isConnected = false;
        private byte[] sendingbuf;
        private byte[] msg;
        
        public Clientsocket(Socket SocketClient)
        {
            this.SocketClient = SocketClient;
            isConnected = true;
        }

        public bool IsConnected => isConnected;

        public string Name
        {
            get => name;
            set => name = value;
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