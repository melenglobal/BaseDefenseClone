using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{   
    [Serializable]
    public class EnemyBossData : Enemy
    {
        public EnemyBossData(int health, int damage, float attackRange, float speed, float chaseSpeed, EnemyType enemyType) : base(health, damage, attackRange, speed, chaseSpeed, enemyType)
        {
        }
    }
}