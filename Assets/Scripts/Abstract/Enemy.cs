using Enums;

namespace Abstract
{
    public abstract class Enemy
    {
        public int Health;

        public int Damage;

        public float AttackRange;

        public float Speed;

        public float ChaseSpeed;

        public int Chase;
        
        public EnemyType EnemyType;

        protected Enemy(int health, int damage, float attackRange, float speed, float chaseSpeed, int chase, EnemyType enemyType)
        {
            Health = health;
            Damage = damage;
            AttackRange = attackRange;
            Speed = speed;
            ChaseSpeed = chaseSpeed;
            Chase = chase;
            EnemyType = enemyType;
        }
        
    }
}