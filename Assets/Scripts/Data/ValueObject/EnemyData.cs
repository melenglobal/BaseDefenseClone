using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class EnemyData : IEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public float AttackRange { get; set; }
        public float Speed { get; set; }
        public float ChaseSpeed { get; set; }
    }
}