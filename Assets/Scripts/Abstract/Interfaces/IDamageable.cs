using UnityEngine;

namespace Abstract
{
    public interface IDamageable
    {   
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        int TakeDamage(int damage);
        Transform GetTransform();
    }
}