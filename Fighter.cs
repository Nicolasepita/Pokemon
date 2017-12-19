using System.Net.Configuration;
using System.Runtime.Remoting.Activation;

namespace Pokemon
{
    public class Fighter
    {
        private string name;
        private int attack;
        private int life;
        private int defence;
        private int speed;
        
        private int level = 1;
        private int xp = 0;
        private int xp_need = 300;
        
        private bool isKO = false;

        public Fighter(string name, int attack, int life, int defence, int speed, int level = 1)
        {
            this.name = name;
            this.attack = attack;
            this.life = life;
            this.defence = defence;
            this.speed = speed;
            this.level = level;
            isKO = life <= 0;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Attack
        {
            get => attack;
            set => attack = value;
        }

        public int Life
        {
            get => life;
            set => life = value;
        }

        public int Defence
        {
            get => defence;
            set => defence = value;
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public bool IsKo
        {
            get => isKO;
            set => isKO = value;
        }

        public void LevelUp(int xp)
        {
            this.xp += xp;
            if (this.xp > xp_need)
            {
                xp -= xp_need;
                xp_need += (4 / 10) * xp_need; //c'est les vraie coef du jeu pokemon
                level++;
                attack += (2 / 10) * attack;
                life += (2 / 10) * Life;
                Program.cons.levelUP_Anim(this);
            }
        }

        public void GetHurt(int damage)
        {
            if ((life -= damage) < 0)
            {
                life = 0;
            }
            isKO = Life <= 0;
        }

        public void Heal(int life)
        {
            this.life += life;
            isKO = life <= 0;
        }
    }
}