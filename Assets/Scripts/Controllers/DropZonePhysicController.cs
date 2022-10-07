using Abstract.Interfaces;
using Abstract.Stackable;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private GemStackerController gemStackerController;
        [SerializeField] private Collider collider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
            
                if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
                {
                    return;
                }
                gemStackerController.GetStack(stackable.SendToStack(),stackable.SendToStack().transform);
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                gemStackerController.OnRemoveAllStack(other.transform);
            }
        }
    }
}