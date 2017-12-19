using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Pokemon
{
    public class PartyConfig
    {
        private Party pa;

        public PartyConfig(Party pa)
        {
            this.pa = pa;
        }
        
        public void StartConfiguration(ConsoleInput ci)
        {
            string ins = "";
            while (ins != "stop" && ins != "start" && pa.PlayersCount < 2 && pa.PlayersCount % 2 != 0)
            {
                ins = ci.read_next_msg();
                if (ins == "")
                {
                    Thread.Sleep(1000);
                    return;
                }
                ins.ToLower();
                string[] insp = ins.Split(' ');
                switch (insp[0])
                {
                    case "gametype":
                        if (insp.Length > 1)
                        {
                            set_game_type(insp[1]);
                        }
                        else
                        {
                            Console.WriteLine("GameType: " + pa.Gametype.GetType());
                        }
                        break;
                    case "ip":
                        if (insp.Length > 1)
                        {
                            setip(insp[1]);
                        }
                        else
                        {
                            Console.WriteLine("IP: " + pa.Ip);
                        }
                    break;
                    case "port":
                        if (insp.Length > 1)
                        {
                            setport(insp[1]);
                        }
                        else
                        {
                            Console.WriteLine("port: " + pa.Port);
                        }
                    break;
                    case "add_local_player":
                        if (insp.Length > 1)
                        {
                            pa.AddPlayer(new Player(insp[1]));
                        }
                        else
                        {
                            Console.WriteLine("can't add player: ''");
                        }
                    break;
                    case "remove_player":
                        if (insp.Length > 1)
                        {    
                           remove_players(insp[1]);
                        }
                        else
                        {
                            Console.WriteLine("can't remove player: ''");
                        }
                    break;
                    case "players":
                        Console.WriteLine("Players list:");
                        foreach (var p in pa.getPlayers)
                        {
                            Console.WriteLine(p.Pseudo + "     " + p.Guid);
                        }
                    break;
                    case "nb_players":
                        if (insp.Length > 1)
                        {
                            setnb_players(insp[1]);
                        }
                        else
                        {
                            Console.WriteLine("nb_players: " + pa.PlayersCount + "   total players: " + pa.Slot);
                        }
                    break;
                    case "start":
                        Console.WriteLine("It seems that some parametre are invalides");
                        Console.WriteLine("Use 'rest' to rest the parametre...");
                    break;
                    case "rest":
                        pa.Gametype = Party.GameType.Local;
                        pa.Ip = IPAddress.Parse("*");
                        pa.Port = 65555;
                        pa.ClearPlayers();
                        Console.WriteLine("Rest Sucess Full");
                    break;
                    default:
                        Console.WriteLine("Sorry, it is unlisabel");
                        help();
                    break;
                }
            }

            if (ins == "stop")
            {
                Program.finish = true;
            }
        }

        private void set_game_type(string s)
        {
            try
            {
                pa.Gametype = (Party.GameType) int.Parse(s);
            }
            catch (Exception e)
            {
                try
                {
                    switch (s)
                    {
                        case "local":
                            pa.Gametype = Party.GameType.Local;
                            break;
                        case "connected":
                            pa.Gametype = Party.GameType.Connected;
                            break;
                        case "server_hosting":
                            pa.Gametype = Party.GameType.Server_Hosting;
                            break;
                        default:
                            throw new ArgumentException("nop");
                    }
                }
                catch (Exception e2)
                {
                    return;
                }
            }
        }

        private void setip(string s)
        {
            if (pa.Gametype == Party.GameType.Local)
            {
                Console.WriteLine("you can't set IP in local mod");
            }
            try
            {
                pa.Ip = IPAddress.Parse(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() + "IP: IP invalide =(");
            }
        }

        private void setport(string s)
        {
            if (pa.Gametype == Party.GameType.Local)
            {
                Console.WriteLine("you can't set port in local mod");
            }
            try
            {
                int porttemp = int.Parse(s);
                if (porttemp < 1024 || porttemp > 49151)
                {
                    Console.WriteLine("port must be betwen 1024 and 49151");
                }
                else
                {
                    pa.Port = porttemp;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() + "port: port invalide =(       (port is a number)");
            }
        }

        private void setnb_players(string s)
        {
            if (pa.Gametype == Party.GameType.Server_Hosting)
            {
                try
                {
                    int nbtemp = int.Parse(s);
                    if (nbtemp % 2 != 0 || nbtemp < 2)
                    {
                        Console.WriteLine("nb_players is a number who: nb % 2 == 0 and nb > 2");
                    }
                    else
                    {
                        pa.Slot = nbtemp;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetType() +
                                      " nb_players: nb_players invalide =(      (nb_players is a number)");
                }
            }
            else
            {
                Console.WriteLine("you must be in server mod to set nb_players");
            }
        }

        private void remove_players(string s)
        {
            if (pa.Gametype == Party.GameType.Server_Hosting)
            {
                try
                {
                    Guid g = Guid.Parse(s);
                    //needed to avoid current modified exception
                    List<Player> player_to_remove = new List<Player>();
                    foreach (var p in pa.getPlayers)
                    {
                        if (p.Guid == g)
                        {
                            player_to_remove.Remove(p);
                        }
                    }
                    foreach (var p in player_to_remove)
                    {
                        bool r = pa.RemovePlayer(p);
                        if (r)
                        {
                            Console.WriteLine("Sucessfully remove: " + p.Pseudo + "   " + p.Guid);
                        }
                    }
                }
                catch (Exception e)
                {
                    //needed to avoid current modified exception
                    List<Player> player_to_remove = new List<Player>();
                    foreach (var p in pa.getPlayers)
                    {
                        if (p.Pseudo == s)
                        {
                            player_to_remove.Remove(p);
                        }
                    }
                    foreach (var p in player_to_remove)
                    {
                        bool r = pa.RemovePlayer(p);
                        if (r)
                        {
                            Console.WriteLine("Sucessfully remove: " + p.Pseudo);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("you must be in server mod to remove a player");
            }
        }

        private void help()
        {
            string[] text =
            {
                "HELP:",
                "",
                "GameType:",
                "1- local: play only on this computer with 2, 4 or more players",
                "2- connected: connect to a local network with 2, 4 or more players to play",
                "3- server_hosting: play on a local network but ",
                "the number of players must be an even number",
                "IP: ",
                "'*' by defalut, is the IP that will be use if your start a server or if you conect to a server",
                "port: ",
                "'65555' by defalut, is the prot that will be use if your start a server or if you conect to a server",
                "players:",
                "is the list of players that will played on the party"
            };
            foreach (var l in text)
            {
                Console.WriteLine(l);
            }
        }
    }
}