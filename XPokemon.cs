using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Pokemon
{
    public class XPokemon : Animal
    {
        public enum XPoketype
        {
            POISON,
            FIRE,
            WATER,
            GRASS,
            ELECTRIC,
            OTHER
        };

        private int level = 1;
        private int damage;
        private int speed;
        private int defense;
        private int attack;
        
        #region Constructor

        public XPokemon(string name = "not found")
        : base(name)
        {
            /* Used to give an empty pokemon for the pokedex to fill. */
        }
        
        public XPokemon(string name, int life, int damage, XPoketype poketype)
        : base(name)
        {
            this.Life = life;
            this.damage = damage;
            this.Poketype = poketype;

            IsKO = life <= 0;
        }

        #endregion Constructor
        

        #region Methods

        public void PPrintStats()
        {
            Console.WriteLine("--- {0} ---", Name);
            Console.WriteLine("\tCurrent HP:");
            Console.WriteLine("\tMax HP: {0}", Life);
            Console.WriteLine("\tAlive: {0}", IsKO);
            Console.WriteLine("\tDamage: {0}", damage);
            Console.WriteLine("\tType: {0}", Poketype);
            Console.WriteLine();
        }
        
        public void LevelUp()
        {
            ++level;
        }

        public void GetHurt(int damage)
        {
            if ((Life -= damage) < 0)
            {
                Life = 0;
            }
            IsKO = Life <= 0;
        }

        public void Heal(int life)
        {
            this.Life += life;
            IsKO = life <= 0;
        }

        public int Life { get; set; }

        public bool IsKO { get; set; }

        public XPoketype Poketype { get; set; }
        
        public int Attack { get; set; }
        
        public int Defense { get; set; }
        
        public int Speed { get; set; }

        #endregion Methods
    }
}