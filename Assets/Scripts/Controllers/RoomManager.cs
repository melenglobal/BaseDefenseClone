using System.Threading.Tasks;
using Abstract.Interfaces;
using Controllers.BaseControllers;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField]
        private RoomTypes roomTypes;
        [SerializeField] 
        private RoomPaymentTextController roomPaymentTextController;
        
        private RoomData _roomData;
        private int _cost;
        private int _payedAmount = 1;
        private bool _canTake;
        private void Start()
        {
            _roomData = GetRoomData();
            _cost = _roomData.Cost;
            SetRoomCost(_cost);
        }
        private RoomData GetRoomData() => BaseSignals.Instance.onSetRoomData(roomTypes);
        private void SetRoomCost(int cost) => roomPaymentTextController.SetInitText(cost);
        public void StartRoomPayment(bool canTake,ICustomer customer)
        {   
            _canTake = canTake;
            if (!_canTake)
                return;
            UpdatePayment(customer);
        }
        public void StopRoomPayment(bool canTake) =>_canTake = canTake;
        private async void UpdatePayment(ICustomer customer)
        {
            if (!_canTake || customer.canPay)
            {
                _canTake = true;
                return;
            }
            if (_cost == 0)
            {
                _roomData.AvailabilityType = AvailabilityType.Unlocked;
            }
            _cost -= _payedAmount; 
            roomPaymentTextController.UpdateText(_cost);
            await Task.Delay(100);
            UpdatePayment(customer);
        }
    }
}