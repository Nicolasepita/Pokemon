using System.Net.Configuration;

namespace Pokemon
{
    public class Fighter
    {
        private string name;
        private int damage;
        private int level = 1;
        private int xp = 0;
        private int xp_need = 300;
        private bool isKO = false;
        private int life;

        public Fighter(string name, int life, int damage)
        {
            this.name = name;
            this.life = life;
            this.damage = damage;
        }
        
        public string Name => name;
        
        public bool IsKo => isKO;

        public int Life => life;

        public int Level => level;
        
        public void LevelUp(int xp)
        {
            this.xp += xp;
            if (this.xp > xp_need)
            {
                xp -= xp_need;
                xp_need += (4 / 10) * xp_need; //c'est les vraie coef du jeu pokemon
                level++;
                damage += (2 / 10) * damage;
                life += (2 / 10) * Life;
                Program.cons.levelUP_Anim(this);
            }
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