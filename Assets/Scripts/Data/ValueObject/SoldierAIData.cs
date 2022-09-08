using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class SoldierAIData: Damageable
    {
        public int AttackRate;

        public SoldierAIData(int damage, int health, int speed) : base(damage, health, speed)
        {
        }
    }
}