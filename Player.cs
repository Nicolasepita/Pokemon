using System;
using Pokemon.Connexion;

namespace Pokemon
{
    public class Player
    {
        public enum Player_type
        {
            Connected,
            Local,
            Disctant_connection,
        }

        private Player_type pt;
        private Guid guid;
        private string pseudo;
        
        private Client co;
        private Connexions connexion;

        private Dresseur d;

        public Player(string pseudo)
        {
            pt = Player_type.Local;
            this.pseudo = pseudo;
            guid = Guid.NewGuid();
            d = new Dresseur(pseudo, 200, 75);
        }

        public Player(Client co)
        {
            pt = Player_type.Disctant_connection;
            guid = Guid.NewGuid();
            ServerCommunication();
        }

        public Player(string pseudo, Connexions connexion)
        {
            pt = Player_type.Connected;
            guid = Guid.NewGuid();
            this.connexion = connexion;
            this.connexion.startClient();
            ClientCommunication();
        }

        private void ServerCommunication()
        {
            co.sendMessage(guid.ToString());
            pseudo = co.getNewMessage();
        }

        private void ClientCommunication()
        {
            guid = Guid.Parse(connexion.getNewMessage());
            connexion.sendMessage(pseudo);
        }
        
        //delegating member

        public Guid Guid => guid;

        public string Pseudo => pseudo;

        public Client Co => co;

        public Connexions Connexion => connexion;
    }
}