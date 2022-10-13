using Controllers.StackableControllers;
using UnityEngine;

namespace Abstract.Stackable
{
    public interface IStackable
    {
        void SetInit(Transform initTransform,Vector3 position);

        void SetVibration(bool isVibrate);

        void SetSound();

        void EmitParticle();
        
        void PlayAnimation();

        GameObject SendToStack();

        void SendStackable(StackableMoney stackable);
        public bool IsSelected { get; set; }

        public bool IsCollected { get; set; }
    }
}