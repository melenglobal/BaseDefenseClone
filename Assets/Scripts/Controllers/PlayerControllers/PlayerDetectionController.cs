﻿using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField] private PlayerManager manager;
        private bool _detectionEnabled=false;
        private void OnTriggerEnter(Collider other)
        {
            if (manager.currentAreaType == AreaType.Base) return;
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if(damagable.IsTaken) return;
                  manager.EnemyList.Add(damagable);
                  if (manager.EnemyTarget == null)
                  {
                      damagable.IsTaken = true;
                      manager.SetEnemyTarget();
                  }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                damagable.IsTaken = false;
                manager.EnemyList.Remove(damagable);
                manager.EnemyList.TrimExcess();
                if (manager.EnemyList.Count == 0)
                {
                    manager.EnemyTarget = null;
                    manager.HasEnemyTarget = false;
                    manager.DamageableEnemy = null;
                }
            }
        }
    }
}