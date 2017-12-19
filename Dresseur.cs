using System.Collections.Generic;

namespace Pokemon
{
    public class Dresseur : Fighter
    {
        private List<Pokemon> pokemons = new List<Pokemon>();
        
        
        public Dresseur(string name, int attack, int life, int defence, int speed, int level=0) : base(name, attack, life, defence, speed, level)
        {
            
        }
        
        //delegating member
        
        public List<Pokemon> Pokemon => pokemons;
        
        public void AddPokemon(Pokemon item)
        {
            pokemons.Add(item);
        }

        public void ClearPokemons()
        {
            pokemons.Clear();
        }

        public bool ContainsPokemon(Pokemon item)
        {
            return pokemons.Contains(item);
        }

        public bool RemovePokemon(Pokemon item)
        {
            return pokemons.Remove(item);
        }

        public int CountPokemon => pokemons.Count;
    }
}