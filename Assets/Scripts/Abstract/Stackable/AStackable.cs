using Abstract.Interfaces;
using Controllers.StackableControllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Abstract.Stackable
{
    public abstract class AStackable : MonoBehaviour, IStackable
    {

        public virtual void SetInit(Transform initTransform, Vector3 position)
        {

        }

        public virtual void SetVibration(bool isVibrate)
        {

        }

        public virtual void SetSound()
        {

        }

        public virtual void EmitParticle()
        {

        }

        public virtual void PlayAnimation()
        {

        }

        public abstract GameObject SendToStack();
        public virtual void SendStackable(StackableMoney stackable)
        {
            DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onSetStackable?.Invoke(stackable));
        }
        

        public virtual bool IsSelected { get; set; }
        public virtual bool IsCollected { get; set; }
    }
}