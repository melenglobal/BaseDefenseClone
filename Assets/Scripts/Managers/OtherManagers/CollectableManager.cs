using System;
using Abstract.Stackable;
using Controllers.StackableControllers;
using Signals;
using UnityEngine;

namespace Managers.OtherManagers
{
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private StackableMoney stackableMoney;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onResetPlayer += OnResetPlayerStack;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onResetPlayer -= OnResetPlayerStack;
        }
        
        private void OnResetPlayerStack()
        {
            stackableMoney.EditPhysics();
        }
    }
}