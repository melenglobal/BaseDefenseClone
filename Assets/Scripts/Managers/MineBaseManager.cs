using System;
using System.Collections.Generic;
using System.Linq;
using Abstract.Interfaces.Pool;
using AI.MinerAI;
using Data.ValueObject;
using Enum;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineBaseManager : MonoBehaviour,IGetPoolObject
    {
        #region Self Variables
        
        #region Serialized Variables
        [SerializeField]
        private Transform instantiationPosition;
        [SerializeField]
        private Transform _gemHolderPosition;
        [SerializeField]
        private List<Transform> minePlaces;
        [SerializeField]
        private List<Transform> cartPlaces;
        
        #endregion

        #region Private Variables

        private Transform _currentMineTarget;
  
        private int _currentWorkerAmount;
     
        public float GemCollectionOffset;
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers=new Dictionary<MinerAIBrain, GameObject>();
        private MineBaseData _mineBaseData;
        

        #endregion

        #endregion
        
        private MineBaseData GetMineBaseData() => InitializeDataSignals.Instance.onLoadMineBaseData?.Invoke();
        private void Awake()
        {
            _mineBaseData = GetMineBaseData();
            GemCollectionOffset=_mineBaseData.GemCollectionOffset;
        }
        
        private void Start()
        {
            InstantiateAllMiners();
            AssignMinerValuesToDictionary();
        }
       

        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _currentWorkerAmount; index++)
            {
                GameObject _currentObject = GetObject(PoolType.MinerWorkerAI);
                _currentObject.transform.position = instantiationPosition.position;
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemCollectionOffset=GemCollectionOffset;
                _mineWorkers.ElementAt(index).Key.GemHolder= _gemHolderPosition;
            }
            
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget += GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos += OnGetGemHolderPos;
        }
       

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
        }

        private Transform OnGetGemHolderPos()
        {
            return _gemHolderPosition;
        }

        #endregion

        public Tuple<Transform,GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex=Random.Range(0, minePlaces.Count + cartPlaces.Count);
            return randomMineTargetIndex>= minePlaces.Count
                ? Tuple.Create(cartPlaces[randomMineTargetIndex % cartPlaces.Count],GemMineType.Cart)
                :Tuple.Create(cartPlaces[randomMineTargetIndex],GemMineType.Mine); 
            
        }


        public GameObject GetObject(PoolType poolName)
        {
           return CoreGameSignals.Instance.onGetObjectFromPool(PoolType.MinerWorkerAI);
        }
    }
}