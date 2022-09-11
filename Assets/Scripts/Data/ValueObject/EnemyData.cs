using System;
using System.Collections.Generic;
using Abstract;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class EnemyData : Enemy
    {
        public List<Transform> TargetList = new List<Transform>();

        public Transform SpawnPosition;
        public EnemyData(int health, int damage, float attackRange, float speed, float chaseSpeed, int chase, EnemyType enemyType) : base(health, damage, attackRange, speed, chaseSpeed, chase, enemyType)
        {
        }
    }
}