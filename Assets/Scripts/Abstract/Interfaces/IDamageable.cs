using UnityEngine;

namespace Abstract
{
    public interface IDamageable
    {   
        bool IsTaken { get; set; }
        bool IsDead { get; set; }
        int TakeDamage(int damage);
        Transform GetTransform();
    }
}