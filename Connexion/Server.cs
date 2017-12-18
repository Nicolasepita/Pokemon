using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Pokemon.Connexion
{
    public class Server
    {
        private List<Client> SocketClients = new List<Client>();
        private Socket SocketServer;
        private IPAddress IP;
        private int port;
        private bool isListening = false;
        
        public Server(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            this.port = port;
        }

        public List<Client> SocketClients1
        {
            get => SocketClients;
            set => SocketClients = value;
        }

        public IPAddress Ip => IP;

        public int Port => port;

        public bool IsListening => isListening;

        public void start()
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IP, port));
            SocketServer.Listen(3);
            SocketServer.BeginAccept(new AsyncCallback(this.connexionAcceptCallback), SocketServer);
            isListening = true;
        }

        private void connexionAcceptCallback(IAsyncResult asyncResult)
        {
            Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketClient = SocketServer.EndAccept(asyncResult);
            SocketClients.Add(new Client(SocketClient));
        }
        
        private void Closeall()
        {
            foreach (var s in SocketClients)
            {
                s.Close();
            }
            SocketServer.Close();
        }
    }
}