using System;
using System.Threading;

namespace Pokemon
{
    internal class Program
    {
        private static bool finish = false;
        
        public static void Main(string[] args)
        {
            try
            {
                Server serv = new Server("127.0.0.1", 6555);
                Connection co = new Connection("127.0.0.1", 6555);
                serv.start();
                Console.WriteLine("server: start");
                co.startClient();
                Console.WriteLine("Client: start");
                co.sendMessage("ceci est un test");
                Console.WriteLine("Client: msg send: ceci est un test");
                while (serv.SocketClients1 == null || !serv.SocketClients1[0].isReady())
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("waiting serv...");
                }
                Console.WriteLine("server: recive msg: " + serv.SocketClients1[0].getNewMessage());
                Console.WriteLine("server: msg recive");
                serv.SocketClients1[0].sendMessage("test");
                Console.WriteLine("server: msg send: test");
                while (!co.isReady())
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("waiting client...");
                }
                Console.WriteLine("Client: recive msg: " + co.getNewMessage());
                Console.WriteLine("Client: msg recive");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Thread.Sleep(100000);
        }
    }
}