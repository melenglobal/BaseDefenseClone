using System;
using System.Collections.Generic;
using Abstract.Stackable;
using Abstract.Stacker;
using UnityEngine;

namespace Concrete
{
    public class MoneyWorkerStackController : AStacker
    {

        public override void SetStackHolder(Transform otherTransform)
        {
            base.SetStackHolder(otherTransform);
        }

        public override void GetStack(GameObject stackable)
        {
            base.GetStack(stackable);
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