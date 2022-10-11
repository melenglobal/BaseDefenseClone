using System;
using System.Collections;
using System.Threading.Tasks;
using Abstract.Interfaces.Pool;
using Controllers.PlayerControllers;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] 
        private PlayerManager manager;

        [SerializeField]
        private Transform weaponHolder;

        private FireController _fireController;
        
        private const float _fireRate = 0.3f;
        
        private void Awake()
        {
            _fireController = new FireController(manager.WeaponTypes); // weapon type oyunda dinamik degisiyor
        }

        
        public void SetEnemyTargetTransform()
        {
            manager.EnemyTarget = manager.EnemyList[0].GetTransform();
            manager.HasEnemyTarget = true;
            Shoot();
        }

        private void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
               SetEnemyTargetTransform();
            }
            else
            {
                manager.HasEnemyTarget = false;
            }
        }

        private void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }

        private void Shoot()
        {
            if(!manager.EnemyTarget) 
                return;
            if (manager.EnemyList[0].IsDead)
            {
                RemoveTarget();
            }
            else
            {
                StartCoroutine(FireBullet());
            }
        }
        private IEnumerator FireBullet()
        {
            yield return new WaitForSeconds(_fireRate);
            _fireController.FireBullets(weaponHolder);
            Shoot();
        }
        private void FireBullet(GameObject bulletPrefab)
        {
            bulletPrefab.transform.rotation = manager.transform.rotation;
            var rigidBodyBullet = bulletPrefab.GetComponent<Rigidbody>();
            rigidBodyBullet.AddForce(manager.transform.forward*40,ForceMode.VelocityChange);
        }
    }
}