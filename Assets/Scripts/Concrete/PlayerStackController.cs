using System.Collections.Generic;
using Abstract;
using Abstract.Stackable;
using Abstract.Stacker;
using UnityEngine;

namespace Concrete
{
    public class PlayerStackController : AStacker
    {
        
        public override void SetStackHolder(Transform otherTransform)
        {
           otherTransform.SetParent(transform);
        }
        
        public override void GetStack(GameObject stackableObj)
        {
           base.GetStack(stackableObj);
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