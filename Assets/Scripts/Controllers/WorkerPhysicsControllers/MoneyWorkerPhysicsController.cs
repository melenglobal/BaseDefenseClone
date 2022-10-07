using Abstract.Interfaces;
using Abstract.Stackable;
using Controllers.StackerControllers;
using UnityEngine;

namespace Controllers.WorkerPhysicsControllers
{   
    [RequireComponent(typeof(Collider))]
    public class MoneyWorkerPhysicsController : MonoBehaviour
    {
        [SerializeField] private MoneyStackerController moneyStackerController;
        [SerializeField] private Collider collider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                moneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
    }
}