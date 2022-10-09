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

        #region Private Variables
        
        private BaseRoomData baseRoomData;
        
        #endregion
        
        #endregion

        private void Awake()
        {
            baseRoomData = GetData();
            SetExistingRooms();
        }
        

        #region Event Subscription
        
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData += OnSetRoomData;
        }
        private void UnsubscribeEvents()
        { 
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData += OnSetRoomData;
        }
        private void OnDisable() => UnsubscribeEvents();
        #endregion  
        private BaseRoomData GetData() => InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke();

        private void SetExistingRooms()
        {
            foreach (var t in baseRoomData.RoomDatas.Where(t => t.AvailabilityType == AvailabilityType.Unlocked))
            {
                ChangeVisibility(t.RoomTypes);
            }
        }
        
        private RoomData OnSetRoomData(RoomTypes roomTypes) => baseRoomData.RoomDatas[(int)roomTypes];
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
        
        private void ChangeVisibility(RoomTypes roomTypes) => extentionController.ChangeExtentionVisibility(roomTypes);
    }
}