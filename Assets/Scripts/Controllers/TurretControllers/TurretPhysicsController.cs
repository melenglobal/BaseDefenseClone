using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.TurretControllers
{
    public class TurretPhysicsController : MonoBehaviour
    {
        [SerializeField] private TurretLocationType turretLocationType;
        [SerializeField] private BoxCollider collider;
        [SerializeField] private TurretMovementController movementController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(typeof(PlayerManager), out var playerManager)) return;
            
            CoreGameSignals.Instance.onSetCurrentTurret?.Invoke(turretLocationType,playerManager.gameObject);
            CoreGameSignals.Instance.onInputHandlerChange?.Invoke(InputHandlers.Turret);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(typeof(PlayerManager), out Component playerManager)) return;
        }
        private void SetCollider(bool isActive)
        {
            collider.enabled = isActive;
        }
    }
}