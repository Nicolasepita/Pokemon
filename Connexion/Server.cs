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
        private List<Client> newclients = new List<Client>();
        
        public Server(IPAddress IP, int port)
        {
            this.IP = IP;
            this.port = port;
        }

        public IPAddress Ip => IP;

        public int Port => port;

        public bool IsListening => isListening;

        public List<Client> SocketClientslist => SocketClients;

        public void AddSocketClients(Client item)
        {
            SocketClients.Add(item);
        }

        public bool RemoveSocketClients(Client item)
        {
            return SocketClients.Remove(item);
        }


        public void start()
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IP, port));
            SocketServer.Listen(3);
            Console.WriteLine("Server start on " + IP + ":" + port);
            SocketServer.BeginAccept(new AsyncCallback(this.connexionAcceptCallback), SocketServer);
            isListening = true;
        }

        private void connexionAcceptCallback(IAsyncResult asyncResult)
        {
            Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketClient = SocketServer.EndAccept(asyncResult);
            Client c = new Client(SocketClient);
            SocketClients.Add(c);
            newclients.Add(c);
        }
        
        private void Closeall()
        {
            foreach (var s in SocketClients)
            {
                s.Close();
            }
            SocketServer.Close();
        }

        public List<Client> Newclients => newclients;

        public void ClearNewclients()
        {
            newclients.Clear();
        }

        public bool RemoveNewclients(Client item)
        {
            return newclients.Remove(item);
        }

        public int CountNewclients => newclients.Count;
    }
}