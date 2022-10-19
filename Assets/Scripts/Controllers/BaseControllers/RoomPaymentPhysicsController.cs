using Abstract.Interfaces;
using Managers.BaseManagers;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] private RoomManager roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            roomManager.StartRoomPayment(customer.CanPay,customer);
        }
        private void OnTriggerExit(Collider other)
        {   
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.CanPay = false;
            roomManager.StopRoomPayment(false);
        }
    }
}