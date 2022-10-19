using System.Collections.Generic;
using Abstract.Interfaces.Pool;
using Data.ValueObject;
using Enum;
using Enums;
using Signals;
using UnityEngine;

namespace Managers.BaseManagers
{
    public class HostageBaseManager : MonoBehaviour,IGetPoolObject
    {
        public List<MinerManager> StackedHostageList=new List<MinerManager>();
        private MinerAnimationStates _currentAnimType=MinerAnimationStates.Idle;
        [SerializeField] private GameObject hostageInstance;
        [SerializeField] private HostageStackController hostageStackController;
        private HostageData _data;
        private int _maxHostileCount;
        private MineBaseData _mineBaseData;
        private List<Transform> _hostagePositionList;
        

        [SerializeField]
        //private HostageStackManager hostageStackManager;
        private void Awake()
        {
            // _data = GetHostageData();
            // _mineBaseData=GetMineBaseData();
            ChangeHostageAnimation(MinerAnimationStates.Crouch);

        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            HostageSignals.Instance.onClearHostageStack += OnClearHostageStack;
            //InputSignals.Instance.onInputTakenActive += OnInputTaken;
            HostageSignals.Instance.onAddHostageStack += OnAddHostageStack;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            //InputSignals.Instance.onInputTakenActive -= OnInputTaken;
            HostageSignals.Instance.onClearHostageStack -= OnClearHostageStack;
            HostageSignals.Instance.onAddHostageStack -= OnAddHostageStack;
        }

        private void OnInputTaken(bool arg0)
        {
            if (arg0)
            {
                ChangeHostageAnimation(MinerAnimationStates.Walk);
            }
            else
            {
                ChangeHostageAnimation(MinerAnimationStates.Idle);
            }
        }

        private void OnAddHostageStack(MinerManager mineManager)
        {
            hostageStackController.AddHostageStack(mineManager);
        }

        #endregion
        // private HostageData GetHostageData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0/*Take from levelManager*/].FrontyardData.HostageData;
        // private MineBaseData GetMineBaseData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0/*Take from levelManager*/].BaseData.MineBaseData;
        private void Start()
        {
            AssignHostileValuesToDictionary();
            InstantiateHostage();
        }
        private void InstantiateHostage()
        {
            int _remainingHostageAmount=_mineBaseData.MaxWorkerAmount - _mineBaseData.CurrentWorkerAmount;
            for (int index = 0; index <_remainingHostageAmount ; index++)
            {
                GetObject(PoolType.Hostage);
                //GameObject hostage=ObjectPoolManager.Instance.GetObject<GameObject>(PoolObjectType.MinerAI.ToString());
                Instantiate(hostageInstance);
                hostageInstance.transform.position = _hostagePositionList[index].position;
               
            }
        }
        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        private void AssignHostileValuesToDictionary()
        {
            _hostagePositionList=_data.HostagePlaces;
        }

        private void OnClearHostageStack(Vector3 centerOfGatePos)
        {
            hostageStackController.SendToGate(centerOfGatePos);
            hostageStackController.ClearStack();
        }
        
        public void ChangeHostageAnimation(MinerAnimationStates hostageAnimationType)
        {
            if (_currentAnimType!=hostageAnimationType)
            {
                
                _currentAnimType = hostageAnimationType;
                foreach (var stackedHostage in StackedHostageList)
                {
                    stackedHostage.ChangeAnimation(hostageAnimationType);
                }
                
            }
          
        }

        public void AddHostageToList(MinerManager hostage)
        {
            StackedHostageList.Add(hostage);
            _currentAnimType = MinerAnimationStates.Crouch;
            ChangeHostageAnimation(MinerAnimationStates.Walk);
        }

       
    }
}