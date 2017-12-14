using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Pokemon;

namespace miniPokemon.Pokemon_game
{
    public class Server
    {
        private List<Clientsocket> SocketClients;
        private Socket SocketServer;
        private IPAddress IP;
        private int port;
        private bool isListening = false;
        
        public Server(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            this.port = port;
        }

        public List<Clientsocket> SocketClients1 => SocketClients;

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
            Socket SocketClient = SocketServer.EndAccept(asyncResult);
            SocketClients.Add(new Clientsocket(SocketClient));
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