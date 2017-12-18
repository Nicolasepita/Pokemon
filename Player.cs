using System;
using Pokemon.Connexion;

namespace Pokemon
{
    public class Player
    {
        private Guid guid;
        private string pseudo;
        private bool local;
        private Client co;

        public Player(string pseudo)
        {
            this.pseudo = pseudo;
            guid = Guid.NewGuid();
            local = true;
        }

        public Player(Client co)
        {
            local = false;
            guid = Guid.Empty;
            Initialise_connection();
        }

        public void Initialise_connection()
        {
            
        }

        public void next_step()
        {
            
        }
        
        
        
        //delegating member

        public Guid Guid => guid;

        public string Pseudo => pseudo;

        public bool Local => local;

        public Client Co => co;
    }
}