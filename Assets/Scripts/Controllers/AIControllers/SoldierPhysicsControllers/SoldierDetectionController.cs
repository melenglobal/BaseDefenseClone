using Abstract;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.AIControllers.SoldierPhysicsControllers
{
    public class SoldierDetectionController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private SoldierAIBrain soldierAIBrain;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damagable)) return;
            if(damagable.IsTaken || damagable.IsDead) return;
            soldierAIBrain.enemyList.Add(damagable);
            damagable.IsTaken = true;
            if (soldierAIBrain.EnemyTarget != null) return;
            soldierAIBrain.EnemyTarget = soldierAIBrain.enemyList[0].GetTransform();
            soldierAIBrain.DamageableEnemy = soldierAIBrain.enemyList[0];
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damagable)) return;
            if(soldierAIBrain.enemyList.Count == 0) return;
            soldierAIBrain.enemyList.Remove(damagable);
            soldierAIBrain.enemyList.TrimExcess();
            if (soldierAIBrain.enemyList.Count == 0)
            {
                soldierAIBrain.EnemyTarget = null;
            }
            damagable.IsTaken = false;
        }
    }
}