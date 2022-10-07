using System.Threading.Tasks;
using Abstract.Interfaces.Pool;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour, IGetPoolObject
    {
        [SerializeField] 
        private PlayerManager manager;

        [SerializeField]
        private Transform weaponHolder;
        public void SetEnemyTargetTransform()
        {
            manager.EnemyTarget = manager.EnemyList[0].GetTransform();
            manager.DamageableEnemy = manager.EnemyList[0];
            manager.HasEnemyTarget = true;
            Shoot();
        }
        public void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
                manager.EnemyTarget = manager.EnemyList[0].GetTransform();
                manager.DamageableEnemy = manager.EnemyList[0];
            }
            else
            {
                manager.HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }
        public async void Shoot()
        {
            if(!manager.EnemyTarget) 
                return;
            if (manager.DamageableEnemy.IsDead)
            {
                RemoveTarget();
            }
            else
            {
                GetObject(PoolType.RifleBullet);
                await Task.Delay(400);
                Shoot();
            }
        }

        public GameObject GetObject(PoolType poolName)
        {
            var bulletPrefab = CoreGameSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            bulletPrefab.transform.position = weaponHolder.position;
            //bulletPrefab.GetComponent<PlayerBulletPhysicsController>().Controller = this;
            FireBullet(bulletPrefab);
            return bulletPrefab;
        }
        private void FireBullet(GameObject bulletPrefab)
        {
            bulletPrefab.transform.rotation = manager.transform.rotation;
            var rigidBodyBullet = bulletPrefab.GetComponent<Rigidbody>();
            rigidBodyBullet.AddForce(manager.transform.forward*40,ForceMode.VelocityChange);
        }
    }
}