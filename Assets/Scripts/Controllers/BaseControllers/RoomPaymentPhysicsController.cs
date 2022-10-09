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
        [SerializeField] 
        private TextMeshPro remainingCostText;
        
        [SerializeField]
        private RoomTypes roomTypes;

        private void Awake()
        {
            SetInitText();
        }

        private void SetInitText()
        {
            var cost = BaseSignals.Instance.onUpdateRoomCostText(roomTypes);
            remainingCostText.text = cost.ToString();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.MakePayment(); 
            UpdateText(-1);
        }
  
        private void UpdateText(int payedAmount)
        {
            var cost=BaseSignals.Instance.onUpdateRoomCostText(roomTypes);
            cost -= payedAmount;
            remainingCostText.text = cost.ToString();
        }
    }
}