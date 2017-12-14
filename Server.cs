using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace miniPokemon.Pokemon_game
{
    public class Server
    {

        private List<Socket> SocketClients;
        private Socket SocketServer;
        private IPAddress IP;
        private int port;
        private byte[] msg;
        
        public Server(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            this.port = port;
        }

        public void start()
        {
            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(new IPEndPoint(IP, 65555));
            SocketServer.Listen(3);
            SocketServer.BeginAccept(new AsyncCallback(this.connexionAcceptCallback), SocketServer);
        }

        private void connexionAcceptCallback(IAsyncResult asyncResult)
        {
            Socket SocketClient = SocketServer.EndAccept(asyncResult);

            while (SocketClient.Available == 0)
            {
                Thread.Sleep(3000);
                Console.WriteLine("Server: Client[0]: waiting for message...");
            }
            
            this.msg = new byte[SocketClient.Available];
            SocketClient.BeginReceive(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), SocketClient);
            
            SocketClients.Add(SocketClient);
        }


        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            string msg = Encoding.UTF8.GetString(this.msg);
            
            Console.WriteLine("Server: Client[0]: recived" + msg);
            //Socket SocketClient = SocketClients[0];
            //int read = SocketClient.EndReceive(asyncResult);
        }
        
        
        private void SendCallback(IAsyncResult asyncResult)
        {
            //rien a faire
            
            //Socket SocketClient = SocketClients[0];
            //int send = SocketClient.EndSend(asyncResult);
            Closeall();
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