using UnityEngine;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;

namespace Managers
{

    public class PoolManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables
        [ShowInInspector]
        private SerializedDictionary<PoolType, PoolData> _data;
        private int _listCountCache;
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

        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetObjectFromPool -= OnGetObjectFromPoolType;
            CoreGameSignals.Instance.onReleaseObjectFromPool -= OnReleaseObjectFromPool;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

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
            ObjectPoolManager.Instance.AddObjectPool<GameObject>(FactoryMethod, TurnOnObject, TurnOffObject, poolType.ToString(), initalAmount, isDynamic);
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

        private GameObject FactoryMethod()
        {
            var go = Instantiate(_data[(PoolType)_listCountCache].ObjectType,transform);
            return go;
        }

    }

}