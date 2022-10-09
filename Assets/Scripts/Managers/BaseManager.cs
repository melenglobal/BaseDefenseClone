using System;
using System.Linq;
using Controllers.BaseControllers;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
       
        [SerializeField] private BaseExtentionController extentionController;
        
        #endregion
    
        #region Public Variables
        
        private BaseRoomData baseRoomData;
        
        #endregion

        #region Private Variables
        
        
        #endregion
        
        #endregion

        private void Awake()
        {
            baseRoomData = GetData();
            SetExistingRooms();
        }
        

        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
            BaseSignals.Instance.onTakePayment += OnTakePayment;
            BaseSignals.Instance.onUpdateRoomCostText += OnUpdateRoomCostText;
        }
        private void UnsubscribeEvents()
        { 
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
            BaseSignals.Instance.onTakePayment -= OnTakePayment;
            BaseSignals.Instance.onUpdateRoomCostText += OnUpdateRoomCostText;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion  
        private BaseRoomData GetData()
        {
            return InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke();
        }

        private void SetExistingRooms()
        {
            foreach (var t in baseRoomData.RoomDatas.Where(t => t.AvailabilityType == AvailabilityType.Unlocked))
            {
                ChangeVisibility(t.RoomTypes);
            }
        }

        private void OnTakePayment(RoomTypes roomTypes, int payedAmount)
        {
            baseRoomData.RoomDatas[(int)roomTypes].Cost -= payedAmount;
        }

        private int OnUpdateRoomCostText(RoomTypes roomTypes)
        {
            return baseRoomData.RoomDatas[(int)roomTypes].Cost;
        }
        private void OnChangeVisibility(RoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
            ChangeAvailabilityType(roomTypes);
        }
        
        private void ChangeAvailabilityType(RoomTypes roomTypes)
        {
            baseRoomData.RoomDatas[(int)roomTypes].AvailabilityType = AvailabilityType.Unlocked;
            InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(baseRoomData);
        }
        
        private void ChangeVisibility(RoomTypes roomTypes)
        {
            extentionController.ChangeExtentionVisibility(roomTypes);
        }
    }
}