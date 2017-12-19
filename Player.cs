using System;
using Pokemon.Connexion;

namespace Pokemon
{
    public class Player
    {
        private Guid guid;
        private string pseudo;
        private bool servmod = false;
        
        private Connexions co;
        private Client cl;

        private Dresseur d;

        public Player(string pseudo, Connexions co)
        {
            this.pseudo = pseudo;
            d = new Dresseur(pseudo, 75, 200, 0, 0);
            ClientCommunication();
        }
        
        public Player(Client co)
        {
            servmod = true;
            guid = Guid.NewGuid();
            d = new Dresseur(pseudo, 75, 200, 0, 0);
            ServerCommunication();
        }
        
        private void ServerCommunication()
        {
            cl.sendMessage(guid.ToString());
            pseudo = cl.getNewMessage();
        }

        private void ClientCommunication()
        {
            guid = Guid.Parse(co.getNewMessage());
            co.sendMessage(pseudo);
        }
        
        //delegating member

        public Guid Guid => guid;

        public string Pseudo => pseudo;

        public bool Servmod => servmod;
        
        public Connexions Co => co;

        public Client Cl => cl;
    }
}