using Abstract;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField] private PlayerManager manager;
        
        private bool _detectionEnabled=false;
        
        private void OnTriggerEnter(Collider other)
        {   
            if (manager.currentAreaType == AreaType.Base) return;
            
            if (!other.TryGetComponent(out IDamageable damagable)) return;
            
            if(damagable.IsTaken) return;
            
            manager.EnemyList.Add(damagable);
            damagable.IsTaken = true;
                  
            if (manager.EnemyTarget != null) return;
            manager.SetEnemyTarget();
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damagable)) return;
            
            damagable.IsTaken = false;
            manager.EnemyList.Remove(damagable);
            manager.EnemyList.TrimExcess();
            
            if (manager.EnemyList.Count != 0) return;
            manager.EnemyTarget = null;
            manager.HasEnemyTarget = false;
        }
    }
}