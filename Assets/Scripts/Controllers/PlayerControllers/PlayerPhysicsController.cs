using Abstract.Interfaces;
using Controllers.BaseControllers;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour,IInteractable
    {   
        #region Self Variables

        #region Serialized Variables,
        
        [SerializeField] private PlayerManager playerManager;

        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {   
                Debug.Log("Gate");
                GateEnter(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                GateExit(other);
            }
        }
        private void GateEnter(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
            gameObject.layer = LayerMask.NameToLayer("Base");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.Battle : AreaType.Base);
        }
        private void GateExit(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
            gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard ? "FrondYard" : "Base");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.Battle : AreaType.Base);
            
            if(!playerIsGoingToFrontYard) return;
            playerManager.HasEnemyTarget = false;
            playerManager.EnemyList.Clear();
        }

    }
}