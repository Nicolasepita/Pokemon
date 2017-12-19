using System;
using System.Threading;
using Pokemon.Connexion;

namespace Pokemon
{
    public class PartyTimer
    {
        public enum Phase
        {
            Creating,
            Connecting,
            Waiting,
            Playing,
        }

        private Phase p = Phase.Creating;
        private Party pa;
        private PartyConfig pm;

        private Connexion.Connexions co;
        private Connexion.Server serv;
        
        private ConsoleInput ci = new ConsoleInput();

        public PartyTimer(Party pa)
        {
            this.pa = pa;
            ci = new ConsoleInput();
            Thread myThread = new Thread(new ThreadStart(ci.Start_input_reading));
        }

        public void StartCreating()
        {
            p = Phase.Creating;
            pm.StartConfiguration(ci);
        }

        public void StartConnection()
        {
            p = Phase.Connecting;
            if (pa.Gametype == Party.GameType.Local)
            {
                return;
            }
            if (pa.Gametype == Party.GameType.Connected)
            {
                co = new Connexion.Connexions(pa.Ip, pa.Port);
                while (!co.IsConnected)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Connecting...");
                }
                Console.WriteLine("Connected!!");
            }
            else if (pa.Gametype == Party.GameType.Server_Hosting)
            {
                serv = new Server(pa.Ip, pa.Port);
                serv.start();
                int i = 0;
                Console.Write("Waiting for players...");
                while (pa.PlayersCount < pa.Slot)
                {
                    Thread.Sleep(1000);
                    foreach (var sock in serv.SocketClientslist)
                    {
                        if (pa.PlayersCount < pa.Slot)
                        {
                            Player p = new Player(sock);
                            pa.AddPlayer(p);
                            Console.WriteLine("New player: " + p.Pseudo + "    (" + pa.PlayersCount+ "/" + pa.Slot + ")");
                            Console.Write("Waiting for players...");
                        }
                        else
                        {
                            sock.Close();
                        }
                    }
                    i++;
                    if (i >= 10 && pa.PlayersCount < pa.Slot)
                    {
                        i = 0;
                        Console.Write(".");
                    }
                }
            }
        }
        
        public void StartWaiting()
        {
            p = Phase.Waiting;if(pa.Gametype == Party.GameType.Connected)
            {
                
            }
            else if (pa.Gametype == Party.GameType.Server_Hosting)
            {
                
            }
        }

        public void Teams()
        {
            if (pa.CountTeamBlue == pa.Slot / 2)
            {
                foreach (var p in pa.getPlayers)
                {
                    if (!pa.ContainsTeamBlue(p) && !pa.ContainsTeamRed(p))
                    {
                        pa.AddTeamRed(p);
                    }
                }
            }
            
            if (pa.CountTeamRed == pa.Slot / 2)
            {
                foreach (var p in pa.getPlayers)
                {
                    if (!pa.ContainsTeamBlue(p) && !pa.ContainsTeamRed(p))
                    {
                        pa.AddTeamBlue(p);
                    }
                }
            }    
        }

        public void StartPlaying()
        {
            p = Phase.Playing;
        }

        public Phase P => p;
    }
}