using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract.Interfaces;
using Abstract.Interfaces.Pool;
using AIBrains.WorkerBrain.MoneyWorker;
using Controllers.AIControllers;
using Controllers.AIControllers.MoneyWorkerAIControllers;
using Controllers.StackableControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers.BaseManagers
{
    public class MoneyWorkerManager : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector] 
        private MoneyWorkerAIData _moneyWorkerAIData;

        [SerializeField] 
        private Transform paymentTarget;
        
        [SerializeField] 
        private MoneyWorkerPaymentTextController paymentTextController;
        
        [ShowInInspector]
        private List<StackableMoney> _targetList = new List<StackableMoney>();
        
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();
        
        [ShowInInspector]
        private List<Vector3> _slotTransformList = new List<Vector3>();
        
        private bool _canTake;
        
        private int _payedAmount = 10;

        private int _workerCost;
        
        // Save Load Data implementasyonu needed - Speed,Capacity,ActiveWorkerCount
        #endregion

        #endregion

        #region Event Subscriptions
        

        private void Start()
        {   
            _moneyWorkerAIData = OnGetWorkerAIData();
            _workerCost = _moneyWorkerAIData.Cost;
            SetInitCost(_workerCost);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData += OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onGetTransformMoney += OnSendMoneyPositionToWorkers;
            MoneyWorkerSignals.Instance.onThisMoneyTaken += OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable += OnAddMoneyPositionToList;
            CoreGameSignals.Instance.onClearActiveLevel += OnLevelClear;
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
            CoreGameSignals.Instance.onClearActiveLevel -= OnLevelClear;
        }

        private void OnLevelClear()
        {
            foreach (var t in _workerList)
            {
                if (t.gameObject.activeInHierarchy)
                {
                    ReleaseObject(t.gameObject, PoolType.MoneyWorkerAI);
                }
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private MoneyWorkerAIData OnGetWorkerAIData()
        {
            return Resources.Load<CD_AI>("Data/CD_AI").MoneyWorkerAIData;
        }

        private void OnAddMoneyPositionToList(StackableMoney pos)
        {
            _targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            if (_targetList.Count == 0)
                return null;

            var targetT = _targetList.OrderBy(t => (t.transform.position - workerTransform.transform.position).sqrMagnitude)
            .Where(t => !t.IsSelected)
            .Take(_targetList.Count - 1) 
            .LastOrDefault();
            targetT.IsSelected = true;
            return targetT.transform;
        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }

        private void OnThisMoneyTaken()
        {
            var removedObj = _targetList.FirstOrDefault(t => t.IsCollected);
            _targetList.Remove(removedObj);
            _targetList.TrimExcess();

            foreach (var t in _workerList.Where(t => t.CurrentTarget==removedObj))
            {
                SendMoneyPositionToWorkers(t.transform);
            }
        }

        public void GetStackPositions(List<Vector3> gridPos)
        {
            foreach (var t in gridPos)
            {
                _slotTransformList.Add(t);
            }
        }

        private void SetWorkerPosition(MoneyWorkerAIBrain workerAIBrain)
        {
            workerAIBrain.SetInitPosition(_slotTransformList[0]);
            _slotTransformList.RemoveAt(0);
            _slotTransformList.TrimExcess();
        }

        [Button("Add Money Worker")]
        private void CreateMoneyWorker()
        {
            var obj = GetObject(PoolType.MoneyWorkerAI) ;
            var objComp = obj.GetComponent<MoneyWorkerAIBrain>();
            _workerList.Add(objComp);
            SetWorkerPosition(objComp);
        }

        [Button("Release Worker")]
        private void ReleaseMoneyWorker()
        {
            if (!_workerList[0]) return;
            var obj = _workerList[0];
            ReleaseObject(obj.gameObject, PoolType.MoneyWorkerAI);
            _workerList.Remove(obj);
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            CoreGameSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        #endregion

        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }

        public void GenerateMoneyWorker()
        {
            CreateMoneyWorker();
        }

        public void StartWorkerPayment(bool canTake,ICustomer customer)
        {
            _canTake = canTake;
            if (!_canTake)
                return;
            UpdatePaymentAsync(customer);
        }
        private void SetInitCost(int cost) => paymentTextController.SetInitText(cost);
        public void StopWorkerPayment(bool canTake) => _canTake = canTake;

        private async Task UpdatePaymentAsync(ICustomer customer) // Value Task,normal task alocationa sebep olur. Unitask // promiseler unity ile entege calisir
        {
            if (_workerCost == 0)
            {
                _canTake = false;
                customer.CanPay = false;
                GenerateMoneyWorker();
                _workerCost = _moneyWorkerAIData.Cost;
                paymentTextController.UpdateText(_workerCost);
                UpdateWorkerData();
            }
            
            if (!_canTake || !customer.CanPay)
            {
                _canTake = true;
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                return;
            }
            customer.PlayPaymentAnimation(paymentTarget);
            _workerCost -= _payedAmount;
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
            paymentTextController.UpdateText(_workerCost);
            await Task.Delay(100);
            UpdatePaymentAsync(customer); // recursion stackoverflow yersin,readability düser,while yap
        }

        private void UpdateWorkerData()
        {
            
        }
    }
}
