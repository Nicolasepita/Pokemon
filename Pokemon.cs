using System;
namespace Pokemon
{
    public class Pokemon : Fighter
    {
        public enum Poketype
        {
            POISON,
            FIRE,
            WATER,
            GRASS,
            ELECTRICK
        };
        
        private Poketype poketype;

        public Pokemon(string name, int life, int damage, Poketype poketype) : base(name, life, damage)
        {
            this.poketype = poketype;
        }
        
        public void WhoAmI()
        {
            Console.WriteLine("I'm a Pokemon");
        }

        public void Describe()
        {
            Console.WriteLine("My name is " + Name + " I'm a pokemon of type " + poketype + " and I'm level " + Level);
        }
    }
}