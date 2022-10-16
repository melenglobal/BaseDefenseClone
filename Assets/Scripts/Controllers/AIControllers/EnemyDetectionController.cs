using Abstract;
using AIBrains.EnemyBrain;
using Controllers.PlayerControllers;
using Controllers.SoldierPhysicsControllers;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class EnemyDetectionController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField]
        private EnemyAIBrain enemyAIBrain;

        #endregion

        #region Private Variables

        private Transform _detectedMine;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController))
            {
                PickOneTarget(other);
                enemyAIBrain.CachePlayer(physicsController);
                enemyAIBrain.CacheSoldier(null);
            }

            if (other.TryGetComponent(out SoldierHealthController soldierHealthController))
            {   
                Debug.Log("Soldier has target !");
                enemyAIBrain.CachePlayer(null);
                PickOneTarget(other);
                enemyAIBrain.CacheSoldier(soldierHealthController);
               
            }
         
        }
        private void OnTriggerExit(Collider other)
        { 
            if (other.TryGetComponent(out PlayerPhysicsController physicsController) )
            {
                enemyAIBrain.SetTarget(null);
                enemyAIBrain.CachePlayer(null);
            }

            if (!other.TryGetComponent(out IDamageable damageable)) return;
            enemyAIBrain.SetTarget(null);
            enemyAIBrain.CacheSoldier(null);
        }
        private void PickOneTarget(Collider other)
        {
            if (enemyAIBrain.CurrentTarget == enemyAIBrain.TurretTarget)
            {
                enemyAIBrain.SetTarget(other.transform);
            }
        }
    }
}