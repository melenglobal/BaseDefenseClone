using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class TurretPhysicsController : MonoBehaviour
    {
        [SerializeField] private TurretLocationType turretLocationType;
        [SerializeField] private BoxCollider collider;
        [SerializeField] private TurretMovementController movementController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(typeof(PlayerManager), out var playerManager)) return;
            
            CoreGameSignals.Instance.onSetCurrentTurret?.Invoke(turretLocationType);
            CoreGameSignals.Instance.onInputHandlerChange?.Invoke(InputHandlers.Turret);
            Debug.Log(InputHandlers.Turret);
            //SetCollider(false);

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