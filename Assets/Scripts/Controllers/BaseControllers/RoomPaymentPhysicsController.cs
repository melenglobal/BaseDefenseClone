using Abstract.Interfaces;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] private RoomManager roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.MakePayment();
            roomManager.StartRoomPayment(customer.canPay,customer);
        }
        private void OnTriggerExit(Collider other)
        {   
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.canPay = false;
            customer.MakePayment();
            roomManager.StopRoomPayment(false);
        }
    }
}