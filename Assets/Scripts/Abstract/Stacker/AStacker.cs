using System;
using System.Collections.Generic;
using Abstract.Stack;
using Abstract.Stackable;
using UnityEngine;

namespace Abstract.Stacker
{
    public abstract class AStacker : MonoBehaviour,IStacker
    {
        public virtual void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public virtual void GetStack(GameObject stackableObj)
        {
            throw new NotImplementedException();
        }
        

        public virtual void GetAllStack(IStack stack)
        {
         
        }

        public virtual void RemoveStack(IStackable stackable)
        {
            
        }

        public virtual void RemoveAllStack(List<IStackable> stackables)
        {
            
        }

        public virtual void ResetStack(IStackable stackable)
        {
            
        }
        
    }
}