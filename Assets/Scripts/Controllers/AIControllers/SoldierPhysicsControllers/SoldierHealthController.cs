using Abstract;
using Abstract.Interfaces;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.AIControllers.SoldierPhysicsControllers
{
    public class SoldierHealthController : MonoBehaviour, IInteractable,IDamageable
    {
        [SerializeField] 
        private SoldierAIBrain soldierAIBrain;
        
        private void OnEnable()
        {
            IsDead = false;
        }

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        public int TakeDamage(int damage)
        {
            soldierAIBrain.Health -= damage;
            if (soldierAIBrain.Health <= 0)
            {
                IsDead = true;
            }

            return soldierAIBrain.Health;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}