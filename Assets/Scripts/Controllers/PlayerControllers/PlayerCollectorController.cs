﻿using Abstract.Interfaces;
using Abstract.Stackable;
using Controllers.Player;
using UnityEngine;
using MoneyStackerController = Controllers.StackerControllers.MoneyStackerController;

namespace Controllers.PlayerControllers
{
    public class PlayerCollectorController : MonoBehaviour
    {
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
    }
}