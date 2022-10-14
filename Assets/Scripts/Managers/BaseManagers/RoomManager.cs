using System.Threading.Tasks;
using Abstract.Interfaces;
using Controllers.BaseControllers;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers.BaseManagers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField]
        private RoomTypes roomTypes;
        [SerializeField] 
        private RoomPaymentTextController roomPaymentTextController;
        
        private RoomData _roomData;
        private int _payedAmount = 10;
        private bool _canTake;
        private void Start()
        {
            _roomData = GetRoomData();
            SetRoomCost(_roomData.Cost);
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
            
            if (_roomData.Cost == 0)
            {
                _canTake = false;
                customer.CanPay = false;
                _roomData.AvailabilityType = AvailabilityType.Unlocked;
                BaseSignals.Instance.onChangeExtentionVisibility(roomTypes);
                UpdateRoomData();
            }
            
            if (!_canTake || !customer.CanPay)
            {
                _canTake = true;
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                return;
            }

            _roomData.Cost -= _payedAmount;
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
            roomPaymentTextController.UpdateText(_roomData.Cost);
            UpdateRoomData();
            await Task.Delay(100);
            UpdatePayment(customer);
        }

        private void UpdateRoomData() => BaseSignals.Instance.onUpdateRoomData(_roomData,roomTypes);
    }
}