using Managers.BaseManagers;
using UnityEngine;

namespace Controllers.AIControllers
{
    public class MoneyWorkerGenerateButtonController : MonoBehaviour
    {
        [SerializeField] 
        private MoneyWorkerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.GenerateSoldier();
            }
        }
    }
    }
}