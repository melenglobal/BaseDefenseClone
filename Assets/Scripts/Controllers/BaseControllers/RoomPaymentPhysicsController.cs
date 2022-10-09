using System;
using Abstract.Interfaces;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] private RoomManager roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.StartPayment();
            roomManager.StartRoomPayment(customer.canPay,customer);
            Debug.Log(customer.canPay);
        }
        private void OnTriggerExit(Collider other)
        {   
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.StopPayment();
            roomManager.StopRoomPayment(false);
        }
    }
}