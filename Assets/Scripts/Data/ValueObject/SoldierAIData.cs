using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class SoldierAIData: Damageable
    {
        public int AttackRate;

        public int AttackRadius;

        public Transform SpawnPoint;

        public SoldierAIData(int damage, int health, int speed) : base(damage, health, speed)
        {
        }
    }
}