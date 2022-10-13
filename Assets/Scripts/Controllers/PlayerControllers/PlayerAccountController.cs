using Abstract.Interfaces;
using Abstract.Stackable;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {
        
        [SerializeField] private PlayerMoneyStackerController playerMoneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {   
                stackable.IsCollected = true;
                MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                playerMoneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                playerMoneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                playerMoneyStackerController.OnRemoveAllStack();
            }
        }

        #region Account

        public bool canPay { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }
        public void MakePayment()
        {
            if (!canPay)
            {
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                Debug.Log("StopPayment");
                return;
            }
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
            Debug.Log("StartPayment");
        }

        #endregion
       
    }
}