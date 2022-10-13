using System.Collections.Generic;
using UnityEngine;
using Signals;
using System.Linq;
using Abstract.Interfaces.Pool;
using AIBrains.WorkerBrain.MoneyWorker;
using Controllers.StackableControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Sirenix.OdinInspector;



namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<StackableMoney> _targetList = new List<StackableMoney>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();
        [ShowInInspector]
        private List<Vector3> _slotTransformList = new List<Vector3>();
        
        // Save Load Data implementasyonu needed - Speed,Capacity,ActiveWorkerCount
        #endregion

        #endregion

        #region Event Subscriptions

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
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private MoneyWorkerAIData OnGetWorkerAIData()
        {
            return Resources.Load<CD_AI>("Data/CD_WorkerAI").MoneyWorkerAIData;
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
    }
}
