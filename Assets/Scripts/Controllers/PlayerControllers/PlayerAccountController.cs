using System;
using Abstract.Interfaces;
using Abstract.Stackable;
using Controllers.BaseControllers;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {
        public SphereCollider Collider;
        
        [SerializeField] private PlayerMoneyStackerController playerMoneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IStackable>(out IStackable stackable)) return;
            stackable.IsCollected = true;
            MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
            playerMoneyStackerController.GetStack(stackable.SendToStack());

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GatePhysicsController>(out GatePhysicsController gatePhysics))
            {
                playerMoneyStackerController.OnRemoveAllStack();
            }
        }

        #region Account

        public bool CanPay { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }

        #endregion
       
    }
}