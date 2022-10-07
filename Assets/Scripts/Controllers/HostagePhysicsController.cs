using Enum;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class HostagePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private MinerManager minerManager;
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player") && minerManager.CurrentType == HostageType.HostageWaiting)
            {
                
                minerManager.AddToHostageStack();
            }
        }
    }
}