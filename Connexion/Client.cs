using System;
using System.Net.Sockets;
using System.Text;

namespace Pokemon.Connexion
{
    public class Client
    {
        private Socket SocketClient;
        private bool isConnected = false;
        private byte[] sendingbuf;
        private byte[] msg;
        
        public Client(Socket SocketClient)
        {
            this.SocketClient = SocketClient;
            isConnected = true;
        }

        public bool IsConnected => isConnected;

        public void sendMessage(string s)
        {
            sendingbuf = Encoding.UTF8.GetBytes(s);
            SocketClient.BeginSend(sendingbuf, 0, sendingbuf.Length, SocketFlags.None, new AsyncCallback(this.SendCallback),
                SocketClient);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            //int send = SocketClient.EndSend(asyncResult);
        }

        public string getNewMessage()
        {
            string msg = null;
            if (isReady())
            {
                this.msg = new byte[SocketClient.Available];
                SocketClient.BeginReceive(this.msg, 0, this.msg.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback),
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