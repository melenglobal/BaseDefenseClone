using Abstract;
using Abstract.Interfaces;
using Data.ValueObject;
using Managers;
using Managers.OtherManagers;
using UnityEngine;

namespace Controllers.BulletControllers
{
    public class BulletPhysicsController : MonoBehaviour,IAttacker
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private BulletManager bulletManager;

        #endregion

        #region Private Variables

        private int _damage;

        #endregion

        #endregion

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }
        public int Damage()
        {
            return _damage;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            bulletManager.SetBulletToPool();
            
        }

    }
    
}