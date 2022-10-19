using Abstract.Interfaces;
using Managers.BaseManagers;
using UnityEngine;

namespace Controllers.AIControllers.MoneyWorkerAIControllers
{
    public class MoneyWorkerGenerateButtonPhysicsController :MonoBehaviour
    {
        [SerializeField] 
        private MoneyWorkerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            manager.StartWorkerPayment(customer.CanPay,customer);
        }
        private void OnTriggerExit(Collider other)
        {   
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.CanPay = false;
            manager.StopWorkerPayment(false);
        }
    }
}