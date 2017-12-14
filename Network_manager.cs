using System;
using System.Net;
using System.Net.Sockets;

namespace Pokemon
{
    public class Network_manager
    {
        private Socket SocketClient;
        private IPAddress IP;
        private int port;
        private byte[] msg;
        
        public Network_manager(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            this.port = port;
        }

        private void startclient()
        {
            SocketClient.BeginConnect(new IPEndPoint(IP, port), new AsyncCallback(connexionConnectCallback), SocketClient);
        }    
        
        private void connexionConnectCallback(IAsyncResult asyncResult)
        {
            SocketClient.EndConnect(asyncResult);
            
            
            
        }
        
        
        
        
    }
}