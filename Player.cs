using System;
using Pokemon.Connexion;

namespace Pokemon
{
    public class Player
    {
        private Guid guid;
        private string pseudo;
        
        private Connexions co;

        private Dresseur d;

        public Player(string pseudo, Connexions co)
        {
            this.pseudo = pseudo;
            guid = Guid.NewGuid();
            d = new Dresseur(pseudo, 200, 75);
            ClientCommunication();
        }
        
        private void ClientCommunication()
        {
            guid = Guid.Parse(co.getNewMessage());
            co.sendMessage(pseudo);
        }
        
        //delegating member

        public Guid Guid => guid;

        public string Pseudo => pseudo;

        public Connexions Co => co;
    }
}