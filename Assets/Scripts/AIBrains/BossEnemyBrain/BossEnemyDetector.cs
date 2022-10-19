using Controllers.PlayerControllers;
using UnityEngine;

namespace AIBrains.BossEnemyBrain
{
    public class BossEnemyDetector : MonoBehaviour
    {
        [SerializeField]
        private BossEnemyBrain bossBrain;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerPhysicsController))
            {
                bossBrain.PlayerTarget = other.transform.parent.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerManager))
            {
                bossBrain.PlayerTarget = null;
            }
        }
    } 
}
