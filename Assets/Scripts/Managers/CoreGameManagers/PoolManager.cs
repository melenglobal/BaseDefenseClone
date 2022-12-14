using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Managers.CoreGameManagers
{

    public class PoolManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables
        [ShowInInspector]
        private SerializedDictionary<PoolType, PoolData> _data;
        private int _listCountCache;

        [SerializeField] private List<Transform> activeObjectTransforms = new List<Transform>();
        #endregion

        #endregion

        private void Awake()
        {
            _data = GetData();
            InitializePools();
            
        }

        #region Event Subscription


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetObjectFromPool += OnGetObjectFromPoolType;
            CoreGameSignals.Instance.onReleaseObjectFromPool += OnReleaseObjectFromPool;
            // CoreGameSignals.Instance.onClearActiveLevel -= OnClearPool;
            // CoreGameSignals.Instance.onLevelInitialize -= OnInitPool;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetObjectFromPool -= OnGetObjectFromPoolType;
            CoreGameSignals.Instance.onReleaseObjectFromPool -= OnReleaseObjectFromPool;
            // CoreGameSignals.Instance.onClearActiveLevel -= OnClearPool;
            // CoreGameSignals.Instance.onLevelInitialize -= OnInitPool;
        }

     
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnInitPool()
        {
            InitializePools();
        }

        // private void OnClearPool()
        // {
        //     var poolCount = System.Enum.GetNames(typeof(EnemyType)).Length;
        //     for (int i = 0; i < poolCount; i++)
        //     {
        //         PoolType poolName = (PoolType)i;
        //         
        //
        //     }
        //    
        // }
        private GameObject OnGetObjectFromPoolType(PoolType poolType)
        {
            _listCountCache = (int)poolType;
           return ObjectPoolManager.Instance.GetObject<GameObject>(poolType.ToString());
        }
        private void OnReleaseObjectFromPool(PoolType poolType, GameObject obj)
        {
            _listCountCache = (int)poolType;
            obj.transform.parent = transform;
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = new Quaternion(0, 0, 0, 0).normalized;
            ObjectPoolManager.Instance.ReturnObject<GameObject>(obj, poolType.ToString());
        }

        private SerializedDictionary<PoolType, PoolData> GetData()
        {
            return Resources.Load<CD_Pool>("Data/CD_Pool").PoolDataDic;
        }

        private void InitializePools()
        {
            for (int index = 0; index < _data.Count; index++)
            {
                _listCountCache = index;
                InitPool(((PoolType)index), _data[((PoolType)index)].initalAmount, _data[((PoolType)index)].isDynamic);
            }

        }

        private void InitPool(PoolType poolType, int initalAmount, bool isDynamic)
        {
            ObjectPoolManager.Instance.AddObjectPool<GameObject>(CreatePoolObject, TurnOnObject, TurnOffObject, poolType.ToString(), initalAmount, isDynamic);
        }


        private void TurnOnObject(GameObject obj)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        private void TurnOffObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        private GameObject CreatePoolObject()
        {
           
            var go = Instantiate(_data[(PoolType)_listCountCache].ObjectType,transform);
            return go;
        }

    }

}