using Abstract;
using Abstract.Interfaces;
using AIBrains.EnemyBrain;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class EnemyPhysicsController : MonoBehaviour,IDamageable
    {
        private void OnEnable()
        {
            IsDead = false;
        }

        [SerializeField]
        private EnemyAIBrain _enemyAIBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        private void OnTriggerEnter(Collider other)
        {

            if (!other.TryGetComponent(out IAttacker attacker)) return;

            var damage = attacker.Damage();
            _enemyAIBrain.Health -= damage;
  
            if (_enemyAIBrain.Health ==0)
            {
                IsDead = true;
            }
        }

        public int TakeDamage(int damage)
        {
            if (_enemyAIBrain.Health <= 0) return 0;
            _enemyAIBrain.Health -= damage;
            
            if (_enemyAIBrain.Health != 0) return _enemyAIBrain.Health;
            
            IsDead = true;
            
            return _enemyAIBrain.Health;

        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}