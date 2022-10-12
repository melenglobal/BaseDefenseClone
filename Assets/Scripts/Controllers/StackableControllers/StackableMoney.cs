using System;
using Abstract.Stackable;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.StackableControllers
{
    public class StackableMoney : AStackable
    {   
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;

        private void OnEnable()
        {
            DOVirtual.DelayedCall(2f, EditPhysics);
            DOVirtual.DelayedCall(2f,() => MoneyWorkerSignals.Instance.onSetMoneyPosition?.Invoke(transform));
        }

        public override void SetInit(Transform initTransform, Vector3 position)
        {
            base.SetInit(initTransform, position);
        }

        public override void SetVibration(bool isVibrate)
        {
            base.SetVibration(isVibrate);
        }

        public override void SetSound()
        {
            base.SetSound();
        }

        public override void EmitParticle()
        {
            base.EmitParticle();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
        }

        public override GameObject SendToStack()
        {
            collider.enabled = false;
            return transform.gameObject;
        }

        private void EditPhysics()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }
}