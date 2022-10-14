using Data.ValueObject;
using UnityEngine;

namespace Managers.BaseManagers
{
    public class MineManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        public MineBaseData _data;

        #endregion

        #endregion


        #region Event Subscribetions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadData;
        }

        private void UnsubscribeEvents()
        {
            //InitializeDataSignals.Instance.onLoadMineBaseData -= OnLoadData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnLoadData(MineBaseData mineBaseData)
        {
            _data = mineBaseData;
        }
    }
}