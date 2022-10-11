using Abstract.Interfaces.Pool;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class FireController : IGetPoolObject
    {
        private WeaponTypes _weaponTypes;
        public FireController(WeaponTypes weaponType)
        {
            _weaponTypes = weaponType;
        }
        public GameObject GetObject(PoolType poolName)
        {
            var obj =  CoreGameSignals.Instance.onGetObjectFromPool.Invoke(poolName);
            return obj;
        }
        public void FireBullets(Transform holderTransform)
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType),_weaponTypes.ToString());
            var bullet = GetObject(poolType);
            bullet.transform.position = holderTransform.position;
            bullet.transform.rotation = holderTransform.rotation;
        }
    }
}