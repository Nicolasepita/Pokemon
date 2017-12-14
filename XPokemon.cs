using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace miniPokemon
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
        private bool isKO;
        private int life;
        private int damage;
        private XPoketype poketype;
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
            this.life = life;
            this.damage = damage;
            this.poketype = poketype;

            isKO = life <= 0;
        }

        #endregion Constructor
        

        #region Methods

        public void PPrintStats()
        {
            Console.WriteLine("--- {0} ---", Name);
            Console.WriteLine("\tCurrent HP:");
            Console.WriteLine("\tMax HP: {0}", life);
            Console.WriteLine("\tAlive: {0}", isKO);
            Console.WriteLine("\tDamage: {0}", damage);
            Console.WriteLine("\tType: {0}", poketype);
            Console.WriteLine();
        }
        
        public void LevelUp()
        {
            ++level;
        }

        public int Attack()
        {
            return damage;
        }

        public void GetHurt(int damage)
        {
            if ((life -= damage) < 0)
            {
                life = 0;
            }
            isKO = life <= 0;
        }

        public void Heal(int life)
        {
            this.life += life;
            isKO = life <= 0;
        }

        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        
        public bool IsKO
        {
            get { return isKO; }
            set { isKO = value; }
        }

        public XPoketype Poketype
        {
            get { return poketype;  }
            set { poketype = value; }
        }

        #endregion Methods
    }
}