using Abstract.Interfaces.Pool;
using Data.ValueObject;
using Managers;

using System.Collections.Generic;
using AIBrains.SoldierBrain;
using Data.UnityObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private Transform tentTransfrom;

        [SerializeField] private GameObject WaitPointPrefab;
        [SerializeField] private GameObject WaitPointsParent;
        [SerializeField] private GameObject frondyardPosition;
        
        #endregion

        #region Private Variables
        
        private MilitaryBaseData _data;
        private SoldierAIData _soldierAIData;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable = true;
        private int _totalAmount;
        private int _soldierAmount;
        private List<GameObject> _soldierList = new List<GameObject>();
        
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        
        private int _tentCapacity;
        #endregion

        #endregion
        
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
            //InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadData;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
           // InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnLoadData(MilitaryBaseData data)
        {
            _data = data;
        }

        private void OnSoldierActivation()
        {
            _isTentAvaliable = true;
        }
        public GameObject GetObject(PoolType poolName)
        {
             var soldierAIPrefab = CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
             var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
             Debug.Log(soldierBrain);
             SetSlotZoneTransformsToSoldiers(soldierBrain);
             
             return soldierAIPrefab;
        }
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_soldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = frondyardPosition.transform;
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
        
        public void UpdateTotalAmount(int Amount)
        {
            if(!_isBaseAvaliable) return;
            if (_totalAmount < _data.BaseCapacity)
            {
                _totalAmount += Amount;
            }
            else
            {
                _isBaseAvaliable = false;
            }
        }
        
        [Button]
        public void UpdateSoldierAmount()
        {
            if(!_isTentAvaliable) return;
            if (_soldierAmount < _data.TentCapacity)
            {
                GetObject(PoolType.SoldierAI);
                _soldierAmount += 1;
            }
            else
            {
                _isTentAvaliable= false;
                _soldierAmount = 0;
            }
        }
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
               _slotTransformList.Add(gridPositionData[i]);
               var obj=  Instantiate(WaitPointPrefab,gridPositionData[i],Quaternion.identity,WaitPointsParent.transform);
            }
        }
        
    }
}
