using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class SoldierAIData: IDamageable
    {
        public int AttackRate;
        
        public int Damage { get; set; }
        
        public int Health { get; set; }
        
        public int Speed { get; set; }
    }
}