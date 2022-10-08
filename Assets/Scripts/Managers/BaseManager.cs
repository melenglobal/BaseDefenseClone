using System;
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
        }
        private void UnsubscribeEvents()
        { 
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
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
            for (int i = 0; i < baseRoomData.RoomDatas.Count; i++)
            {
                if (baseRoomData.RoomDatas[i].AvailabilityType == AvailabilityType.Unlocked)
                {
                    ChangeVisibility(baseRoomData.RoomDatas[i].RoomTypes);
                }
                
            }
        }
       
        
        private void OnChangeVisibility(RoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
        }
        private void ChangeVisibility(RoomTypes roomTypes)
        {   
            Debug.Log("ChangeVisibility");
            extentionController.ChangeExtentionVisibility(roomTypes);
        }
    }
}