using Abstract;
using AIBrains.EnemyBrain;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour,IDamageable
    {
        private Transform _detectedPlayer;
        private Transform _detectedMine;
        private EnemyAIBrain _enemyAIBrain;
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                
                _enemyAIBrain.SetTarget(other.transform.parent);
            }

            // /if (other.GetComponent<Mine>())
            // {
            //     _detectedMine = other.GetComponent<Mine>();
            // }/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                _enemyAIBrain.SetTarget(null);
            }

            // /if (other.GetComponent<Mine>())
            // {
            //
            // }/
        }

        public Vector3 GetNearestPosition(GameObject gO)
        {
            return gO?.transform.position ?? Vector3.zero;
        }

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        public int TakeDamage(int damage)
        {
            return 1;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}