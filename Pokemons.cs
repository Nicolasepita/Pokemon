using System;
namespace Pokemon
{
    public class Pokemons
    {
        public enum Poketype
        {
            POISON,
            FIRE,
            WATER,
            GRASS,
            ELECTRICK
        };

        private string name;
        private Poketype poketype;
        private int damage;
        private int level = 1;
        private bool isKO = false;
        private int life;
        
        
        public Pokemons(string name, int life, int damage, Poketype poketype)
        {
            this.name = name;
            this.poketype = poketype;
            this.damage = damage;
            this.life = life;
        }
        
        public string Name => name;
        
        public bool IsKo => isKO;

        public int Life => life;
        
        public void WhoAmI()
        {
            Console.WriteLine("I'm a Pokemon");
        }

        public void Describe()
        {
            Console.WriteLine("My name is " + Name + " I'm a pokemon of type " + poketype + " and I'm level " + level);
        }
        
        public void LevelUp()
        {
            level++;
        }

        public int Attack()
        {
            return damage;
        }

        public void GetHurt(int damage)
        {
            life -= damage;
            isKO = life <= 0;
        }

        public void Heal(int life)
        {
            this.life += life;
            isKO = life <= 0;
        }
    }
}