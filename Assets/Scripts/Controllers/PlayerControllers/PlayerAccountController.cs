using Abstract.Interfaces;
using Abstract.Stackable;
using UnityEngine;


namespace Controllers.PlayerControllers
{
    public class PlayerAccountController : MonoBehaviour,ICustomer
    {
        private int _moneyAmount = 100000;
        private int _gemAmounth;
        [SerializeField] private PlayerMoneyStackerController playerMoneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                playerMoneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                playerMoneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                playerMoneyStackerController.OnRemoveAllStack();
            }
        }

        public bool canPay { get => _moneyAmount!=0; set { } }
        public int StartPayment()
        {
            return -1;
        }

        public void StopPayment()
        {
            if (_moneyAmount == 0)
            {
                canPay = false;
            }
            
        }
    }
}