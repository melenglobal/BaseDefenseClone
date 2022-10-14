using Signals;
using UnityEngine;

namespace Controllers.BaseControllers
{
    public class MilitaryInitSoldierButtonController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AISignals.Instance.onSoldierAmountUpgrade?.Invoke();
            }
        }
    }
}