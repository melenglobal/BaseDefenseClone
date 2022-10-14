using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIBrains.SoldierBrain;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers.BaseManagers
{
    public class MilitaryBaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private Transform tentTransfrom;
        
        [SerializeField] 
        private Transform slotTransform;

        [SerializeField]
        private Transform frontYardPosition;
        
        [SerializeField]
        private GameObject slotZonePrefab;
        #endregion

        #region Private Variables
        
        private MilitaryBaseData _data;
        private SoldierAIData _soldierAIData;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable=true;
        private int _totalAmount;
        private int _soldierAmount;
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        private int _tentCapacity;
        private List<SoldierAIBrain> _soldierList=new List<SoldierAIBrain>();

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBaseData();
        }
        private MilitaryBaseData GetBaseData()
        {
          return InitializeDataSignals.Instance.onLoadMilitaryBaseData.Invoke();
        }
        public IEnumerator Start()
        {
            if (_data.CurrentSoldierAmount == 0)
                yield break;
            yield return new WaitForSeconds(1f);
            StartCoroutine(SoldierEnumerator());
            yield return new WaitForSeconds(3f);
            StopCoroutine(SoldierEnumerator());
        }
        private IEnumerator SoldierEnumerator()
        {
            OnSoldiersInit(_data.CurrentSoldierAmount);
            yield return null;
        }
        private void OnSoldiersInit(int soldierCount)
        {
            for (var i = 0; i < soldierCount; i++)
            {
                GetSoldier();
            }
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
            AISignals.Instance.onSoldierAmountUpgrade += OnSoldierAmountUpgrade;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
            AISignals.Instance.onSoldierAmountUpgrade -= OnSoldierAmountUpgrade;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        private void OnSoldierActivation()
        {
            var soldierCount = _soldierList.Count-1;
            for (var i = 0; i < soldierCount+1; i++)
            {
                _soldierList[soldierCount-i].HasSoldiersActivated = true;
                _soldierList.RemoveAt(soldierCount - i);
                _soldierList.TrimExcess();
            }
            _isTentAvaliable = true;
            _data.CurrentSoldierAmount = 0;
        }
        private void GetSoldier()
        {
            var soldierAIPrefab = GetObject(PoolType.SoldierAI);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            _soldierList.Add(soldierBrain);
            SetSlotZoneTransformsToSoldiers(soldierBrain);
        }
        
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_data.CurrentSoldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = frontYardPosition;
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
        private void OnSoldierAmountUpgrade()
        {
            UpdateSoldierAmount();
        }
        [Button]
        private async void UpdateSoldierAmount()
        {
            if(!_isTentAvaliable) return;
            if (_data.CurrentSoldierAmount < _data.TentCapacity)
            {
                GetSoldier();
                _data.CurrentSoldierAmount += 1;
                await Task.Delay(100);
                UpdateSoldierAmount();
            }
            else
            {
                _isTentAvaliable= false;
                _data.CurrentSoldierAmount = 0;
            }
        }
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            foreach (var t in gridPositionData)
            {
                _slotTransformList.Add(t);
                var obj=  Instantiate(slotZonePrefab,t,Quaternion.identity,slotTransform);
            }
        }

        #region Pool Signals
       
        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        #endregion
        
        #region SaveSignals
        
        [Button]
        private void SaveData()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }
        private void OnApplicationQuit()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }
        #endregion
        
    }
}
