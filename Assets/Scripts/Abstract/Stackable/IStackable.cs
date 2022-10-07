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
    }
}