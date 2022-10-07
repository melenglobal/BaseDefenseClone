using System;
using Abstract.Stackable;
using Concrete;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {   
        [SerializeField]
        private PlayerStackController playerStackController;

        [SerializeField] private PlayerManager playerManager;
        public AreaType AreaType;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate"))
            {

                if (other.transform.position.z > transform.position.z)
                {
                    AreaType = AreaType.Base;
                }
                else
                {
                    AreaType = AreaType.Base;
                }
            
            }

            if (other.GetComponent<IStackable>() != null)
            {
                playerStackController.SetStackHolder(other.transform);
                
            }
            if (other.TryGetComponent(typeof(TurretPhysicsController),out Component physicController))
            {   
                
                
            }

            if (other.CompareTag("AmmoSpawner"))
            {
                playerManager.IsEnterAmmoCreater(other.gameObject.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Gate"))
            {
               
               if (other.transform.position.z < transform.position.z)
               {
                   AreaType = AreaType.Battle;
               }
               else
               {
                   AreaType = AreaType.Base;
              
               }

            }

            if (other.TryGetComponent(typeof(TurretPhysicsController),out Component physicController))
            {
                
            }
            
        }
    }
}