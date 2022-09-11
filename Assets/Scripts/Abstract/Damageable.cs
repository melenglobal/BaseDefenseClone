namespace Abstract
{
    public abstract class Damageable
    {
        public int Damage;

        public int Health;

        public int Speed;

        protected Damageable(int damage, int health, int speed)
        {
            Damage = damage;
            Health = health;
            Speed = speed;
        }
    }
}