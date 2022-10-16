using System;
using Abstract.Stackable;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.StackableControllers
{
    public class StackableMoney : AStackable
    {   
        
        public override bool IsSelected { get; set; }
        public override bool IsCollected { get; set; }
        
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;

        private void OnEnable()
        {
            EditPhysics();
            DOVirtual.DelayedCall(2f,() => MoneyWorkerSignals.Instance.onSetStackable?.Invoke(this));
        }

        public override GameObject SendToStack()
        {
            collider.enabled = false;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            return transform.gameObject;
        }
        
        public void EditPhysics()
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            collider.enabled = true;
        }
    }
}