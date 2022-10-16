using Signals;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class MilitaryInitSoldierButtonController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            AISignals.Instance.onSoldierAmountUpgrade?.Invoke();
            Debug.Log("Player Enter");
        }
    }
}