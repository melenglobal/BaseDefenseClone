using Abstract.Interfaces.Pool;
using Controllers.BulletControllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers.OtherManagers
{
    public class BulletManager : MonoBehaviour,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private WeaponTypes weaponType;
        
        [SerializeField] 
        private BulletPhysicsController physicsController;

        #endregion

        #region Private Variables

        private WeaponData _data;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetBulletData();
            SetDataToControllers();
        }
        private void OnEnable()
        {
            Invoke(nameof(SetBulletToPool),1f);
        }

        private WeaponData GetBulletData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponDatas[(int)weaponType];
        private void SetDataToControllers() => physicsController.GetData(_data);
        public void ReleaseObject(GameObject obj, PoolType poolName)=>CoreGameSignals.Instance.onReleaseObjectFromPool.Invoke(poolName,obj);
        public void SetBulletToPool()
        {
            var poolName = (PoolType)System.Enum.Parse(typeof(PoolType),weaponType.ToString());
            ReleaseObject(gameObject,poolName);
        }
    }
}