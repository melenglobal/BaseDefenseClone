using Enums;
using Managers.CoreGameManagers;
using Signals;
using UnityEngine;

namespace Controllers.TurretControllers
{
    public class TurretPhysicsController : MonoBehaviour
    {
        [SerializeField] private TurretLocationType turretLocationType;
        [SerializeField] private TurretShootController turretShootController;
       
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(typeof(PlayerManager), out var playerManager)) return;
            
            CoreGameSignals.Instance.onSetCurrentTurret?.Invoke(turretLocationType,playerManager.gameObject);
            InputSignals.Instance.onInputHandlerChange?.Invoke(InputHandlers.Turret);
            turretShootController.readyToAttack = true;
            turretShootController.EnLargeDetectionRadius();
        }
        private void OnTriggerExit(Collider other)
        {
            
            if (!other.TryGetComponent(typeof(PlayerManager), out Component playerManager)) return;
            turretShootController.readyToAttack = false;
            turretShootController.DeSizeDetectionRadius();
        }

    }
}