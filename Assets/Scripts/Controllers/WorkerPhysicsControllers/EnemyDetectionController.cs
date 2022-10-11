using AIBrains.EnemyBrain;
using Controllers.PlayerControllers;
using Managers;
using UnityEngine;

namespace Controllers.WorkerPhysicsControllers
{
    public class EnemyDetectionController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyAIBrain enemyAIBrain;

        #endregion
        #region Private Variables
        
        private bool _amAIDead = false;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerPhysicsController physicsController)) return;
            enemyAIBrain.SetTarget(other.transform.parent);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out PlayerPhysicsController physicsController)) return;
            enemyAIBrain.CurrentTarget = null;
            enemyAIBrain.SetTarget(null);
        }
    }
}