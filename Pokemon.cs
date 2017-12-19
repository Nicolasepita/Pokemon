using System;
using System.Runtime.Remoting.Activation;

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
            ELECTRIC,
            OTHER
        };
        
        private Poketype poketype;

        public Pokemon(string name = "not found") : base(name, 0, 0, 0, 0)
        {
            
        }
        
        public Pokemon(string name, int attack, int life, int defence, int speed, Poketype poketype, int level = 1) : base(name, attack, life, defence, speed, level)
        {
            this.poketype = poketype;
        }

        public Poketype Poketype_
        {
            get => poketype;
            set => poketype = value;
        }

        public void WhoAmI()
        {
            Console.WriteLine("I'm a Pokemon");
        }

        public void Describe()
        {
            Console.WriteLine("My name is " + Name + " I'm a pokemon of type " + poketype + " and I'm level " + Level);
        }
        
        public void PPrintStats()
        {
            Console.WriteLine("--- {0} ---", Name);
            Console.WriteLine("\tCurrent HP:");
            Console.WriteLine("\tMax HP: {0}", Life);
            Console.WriteLine("\tAlive: {0}", IsKo);
            Console.WriteLine("\tDamage: {0}", Attack);
            Console.WriteLine("\tType: {0}", poketype);
            Console.WriteLine();
        }
    }
}