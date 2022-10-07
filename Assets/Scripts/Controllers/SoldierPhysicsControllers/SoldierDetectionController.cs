using Abstract;
using AIBrains.SoldierBrain;
using UnityEngine;

namespace Controllers.SoldierPhysicsControllers
{
    public class SoldierDetectionController : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private SoldierAIBrain soldierAIBrain;

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if (damagable.IsTaken) return;
                soldierAIBrain.enemyList.Add(damagable);
                if (soldierAIBrain.EnemyTarget == null)
                {
                    damagable.IsTaken = true;
                    soldierAIBrain.SetEnemyTargetTransform();
                    Debug.Log("Enter");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if (soldierAIBrain.enemyList.Count == 0)
                {
                    soldierAIBrain.EnemyTarget = null;
                }
                
                Debug.Log("EXIT");
                damagable.IsTaken = false;
                soldierAIBrain.enemyList.Remove(damagable);
                soldierAIBrain.enemyList.TrimExcess();
            }

        }
    }
}