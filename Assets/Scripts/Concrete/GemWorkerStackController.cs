using Abstract.Stackable;
using Abstract.Stacker;
using UnityEngine;

namespace Concrete
{
    public class GemWorkerStackController:AStacker
    {
        public override void SetStackHolder(Transform otherTransform)
        {
            base.SetStackHolder(otherTransform);
        }
        
        public override void RemoveStack(IStackable stackable)
        {
            base.RemoveStack(stackable);
        }
        public override void ResetStack(IStackable stackable)
        {
            base.ResetStack(stackable);
        }
        
    }
}