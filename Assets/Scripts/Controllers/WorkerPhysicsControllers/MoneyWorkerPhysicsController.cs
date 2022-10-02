using System;
using Abstract.Interfaces;
using Commands.EnvironmentCommands;
using Concrete;
using Controllers.StackerControllers;
using UnityEngine;

namespace Controllers.WorkerPhysicsControllers
{   
    [RequireComponent(typeof(Collider))]
    public class MoneyWorkerPhysicsController : MonoBehaviour
    {
        [SerializeField] private MoneyStackerController moneyStackerController;
        [SerializeField] private CapsuleCollider capsuleCollider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ITriggerEnter>(out ITriggerEnter trigger))
            {
                moneyStackerController.SetStackHolder(trigger.TriggerEnter().transform);
                moneyStackerController.GetStack(trigger.TriggerEnter());
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {   
              
                moneyStackerController.OnRemoveAllStack();
                capsuleCollider.enabled = false;

            }
        }

    
    }
}