using Managers;
using UnityEngine;

namespace Buyablezone
{
    public class BuyableZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private BuyableZoneManager buyableZoneManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                buyableZoneManager.TextBounceEffectActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                buyableZoneManager.StartButtonEvent();
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            buyableZoneManager.Purchase.InitialTimer = 0;
            buyableZoneManager.TextBounceEffectActive(false);
            buyableZoneManager.StartRadialProgress( buyableZoneManager.Purchase.InitialTimeOffset, buyableZoneManager.Purchase.InitialTimeOffset);
            
        }
    }
}