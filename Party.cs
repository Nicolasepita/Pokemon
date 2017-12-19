using System.Collections.Generic;
using System.Net;

namespace Pokemon
{
    public class Party
    {
        public enum GameType
        {
            Local,
            Connected,
            Server_Hosting
        };
        
        protected GameType gametype = GameType.Local;
        protected IPAddress ip = IPAddress.Parse("*");
        protected int port = 45555;
        protected int slot = 2;
        
        protected List<Player> players = new List<Player>();
        protected List<Player> TeamBlue = new List<Player>();
        protected List<Player> TeamRed = new List<Player>();
        
        //delegating member

        public GameType Gametype
        {
            get => gametype;
            set => gametype = value;
        }

        public IPAddress Ip
        {
            get => ip;
            set => ip = value;
        }

        public int Port
        {
            get => port;
            set => port = value;
        }

        public int Slot
        {
            get => slot;
            set => slot = value;
        }

        public List<Player> getPlayers => players;
        
        public List<Player> getTeamBlue => TeamBlue;

        public List<Player> getTeamRed => TeamRed;
        
        //list of all player
        
        public void AddPlayer(Player item)
        {
            players.Add(item);
        }

        public void ClearPlayers()
        {
            players.Clear();
        }

        public bool ContainsPlayer(Player item)
        {
            return players.Contains(item);
        }

        public bool RemovePlayer(Player item)
        {
            return players.Remove(item);
        }

        public int PlayersCount => players.Count;

        //team red
        
        public void AddTeamRed(Player item)
        {
            TeamRed.Add(item);
        }

        public void ClearTeamRed()
        {
            TeamRed.Clear();
        }

        public bool ContainsTeamRed(Player item)
        {
            return TeamRed.Contains(item);
        }

        public bool RemoveTeamRed(Player item)
        {
            return TeamRed.Remove(item);
        }

        public int CountTeamRed => TeamRed.Count;
        
        //team blue
        
        public void AddTeamBlue(Player item)
        {
            TeamBlue.Add(item);
        }

        public void ClearTeamBlue()
        {
            TeamBlue.Clear();
        }

        public bool ContainsTeamBlue(Player item)
        {
            return TeamBlue.Contains(item);
        }

        public bool RemoveTeamBlue(Player item)
        {
            return TeamBlue.Remove(item);
        }

        public int CountTeamBlue => TeamBlue.Count;
    }
}