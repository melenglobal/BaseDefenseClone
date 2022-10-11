using Abstract.Interfaces;
using Commands.EnvironmentCommands;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class GatePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,

        [SerializeField] private GateCommand gateCommand;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;
                gateCommand.GateOpen(true);
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable))
                gateCommand.GateOpen(false);
        }
    }
}