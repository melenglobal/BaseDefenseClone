using System.Collections.Generic;
using UnityEngine;
using Signals;
using System.Linq;
using Abstract.Interfaces.Pool;
using AIBrains.WorkerBrain.MoneyWorker;
using Enums;
using Sirenix.OdinInspector;



namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour ,IGetPoolObject, IReleasePoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<Transform> targetList = new List<Transform>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> workerList = new List<MoneyWorkerAIBrain>();

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetTransformMoney += OnSendMoneyPositionToWorkers;
            MoneyWorkerSignals.Instance.onThisMoneyTaken += OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetMoneyPosition += OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.OnMyMoneyTaken += OnMyMoneyTaken;
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetMoneyPosition -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
            MoneyWorkerSignals.Instance.OnMyMoneyTaken -= OnMyMoneyTaken;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        // private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        // {
        //     return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        // }

        private void OnAddMoneyPositionToList(Transform pos)
        {
            targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            /*if (workerList[0].transform == workerTransform)
            {
                var _targetT = targetList.OrderBy(t => Vector3.Distance(t.transform.position, workerTransform.position))
                .Take(1)
                .FirstOrDefault();
                targetList.Remove(_targetT);
                Debug.Log("worker 0");
                return _targetT;
            }
            else if (workerList[1].transform == workerTransform)
            {*/
                var targetMoney = targetList.OrderBy(t => Vector3.Distance(t.transform.position, workerTransform.position))
                .Take(targetList.Count)
                .OrderBy(t => UnityEngine.Random.Range(0,int.MaxValue))
                .LastOrDefault();
                targetList.Remove(targetMoney);
                return targetMoney;
            /*}
            else
            {
                int randomIndex = Random.Range(10,targetList.Count-10);
                var _targetT = targetList.OrderBy(t => Vector3.Distance(t.transform.position, workerTransform.position))
                .Take(1)
                .OrderBy(t => randomIndex)
                .FirstOrDefault(); 
                targetList.Remove(_targetT);
                return _targetT;
            }*/

        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }

        private void OnThisMoneyTaken(Transform gO)
        {
            foreach (var t in workerList)
            {
                if (t.CurrentTarget == gO)
                {
                    SendMoneyPositionToWorkers(t.transform);
                }
                else
                {
                    if (targetList.Contains(gO))
                    {
                        targetList.Remove(gO);
                    }
                }
            }
        }

        private Transform OnMyMoneyTaken(Transform gameOTransform, Transform workerTransform)
        {
            return targetList.Contains(gameOTransform) ? gameOTransform : OnSendMoneyPositionToWorkers(workerTransform);
        }
        [Button("Add Money Worker")]
        private void CreateMoneyWorker()
        {
            var obj = GetObject(PoolType.MoneyWorkerAI) ;
            workerList.Add(obj.GetComponent<MoneyWorkerAIBrain>());
        }
        [Button("Release Worker")]
        private void ReleaseMoneyWorker()
        {
            if (!workerList[0]) return;
            var obj = workerList[0];
            ReleaseObject(obj.gameObject, PoolType.MoneyWorkerAI);
            workerList.Remove(obj);
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
