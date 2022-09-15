using System;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
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
                    AreaType = AreaType.Battle;
                }
            
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