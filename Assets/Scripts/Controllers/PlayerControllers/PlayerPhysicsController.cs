using System;
using Abstract.Stackable;
using Concrete;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {   
        [SerializeField]
        private PlayerStackController playerStackController;
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
            
        }
    }
}