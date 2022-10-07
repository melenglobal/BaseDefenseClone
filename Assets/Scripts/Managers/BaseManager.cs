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
        
        public BaseRoomData BaseRoomData;
        
        #endregion
    
        #region Public Variables

        #endregion

        #region Private Variables
        
        
        #endregion
        
        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {   
           // InitializeDataSignals.Instance.onLoadBaseRoomData += OnLoadData;
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
        }
        private void UnsubscribeEvents()
        {   
           // InitializeDataSignals.Instance.onLoadBaseRoomData -= OnLoadData;
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion  
        private void OnLoadData(BaseRoomData baseRoomData)
        {
            BaseRoomData = baseRoomData;
        }
        
        private void OnChangeVisibility(RoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
        }
        private void ChangeVisibility(RoomTypes roomTypes)
        {
            extentionController.ChangeExtentionVisibility(roomTypes);
        }
    }
}