using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{   
    [Serializable]
    public class EnemyData : Enemy
    {
        public EnemyData(int health, int damage, float attackRange, float speed, float chaseSpeed, int chase, EnemyType enemyType) : base(health, damage, attackRange, speed, chaseSpeed, chase, enemyType)
        {
        }
    }
}