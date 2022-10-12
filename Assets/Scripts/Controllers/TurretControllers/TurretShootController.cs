using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract;
using Controllers.PlayerControllers;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Controllers.TurretControllers
{
    public class TurretShootController : MonoBehaviour
    {
        [SerializeField] private TurretLocationType turretLocationType;
        [SerializeField] private FireController fireController;
        [SerializeField] private WeaponTypes weaponType = WeaponTypes.TurretBullet;
        [SerializeField] private List<GameObject> damageables;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] public CapsuleCollider DetectionCollider;

        public bool readyToAttack { get; set; }
        
        private const float _fireRate = 0.3f;

        private float maxRadius = 14f;

        private float minRadius = 0f;

        private const float stepRate = 0.5f;

        private const float _tolerance = 0.001f;
        

        private void Awake()
        {
            fireController = new FireController(weaponType);
        }

        public void EnemyInRange(GameObject damageable)
        {
            damageables.Add(damageable);
        }

        public void EnemyOutOfRange(GameObject damageable)
        {
            damageables.Remove(damageable);
            damageables.TrimExcess();
        }

        public void RemoveTarget()
        {
            if (damageables.Count == 0) return;
            damageables.RemoveAt(0);
            damageables.TrimExcess();
        }
        

        public void ShootTheTarget()
        {
            if (!readyToAttack)
                return;
            
            if (damageables.Count != 0) // Check bullet count
            {
                // Vector3 direction = damageables[0].GetTransform().position - transform.position;
                //
                // Quaternion enemyRotation = Quaternion.LookRotation(direction, Vector3.up);
                StartCoroutine(FireBullet());
            }
        }

        public async void EnLargeDetectionRadius()
        {
            if (Math.Abs(maxRadius - minRadius) < _tolerance)
            {
                minRadius = 0f;
                return;
            }
            if (minRadius < maxRadius)
            {
                minRadius += stepRate;
                DetectionCollider.radius = minRadius;
            }

            await Task.Delay(100);
            EnLargeDetectionRadius();
        }

        public async void DeSizeDetectionRadius()
        {
            if (Math.Abs(maxRadius - minRadius) < _tolerance)
            {
                maxRadius = 14f;
                return;
            }
            if (maxRadius > minRadius)
            {   
                maxRadius -= stepRate;
                DetectionCollider.radius = maxRadius;
            }

            await Task.Delay(100);
            DeSizeDetectionRadius();
        }
        

        private IEnumerator FireBullet()
        {
            yield return new WaitForSeconds(_fireRate);
                fireController.FireBullets(weaponHolder);
                ShootTheTarget();
        }
    }
}