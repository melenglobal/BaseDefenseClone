using Abstract.Stackable;
using AIBrains.WorkerBrain.MoneyWorker;
using Controllers.AIStackerControllers;
using Signals;
using UnityEngine;

namespace Controllers.WorkerPhysicsControllers
{
    public class MoneyWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerAIBrain moneyWorkerBrain;
        [SerializeField]
        private AIMoneyStackerController moneyStackerController;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                if (moneyWorkerBrain.IsAvailable())
                {
                    MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke(other.transform);
                    moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                    moneyStackerController.GetStack(other.gameObject);
                    moneyWorkerBrain.SetCurrentStock();
                }
            }

            if (!other.CompareTag("Gate")) return;
            moneyStackerController.OnRemoveAllStack();
            moneyWorkerBrain.RemoveAllStock();
        }
    } 
}
