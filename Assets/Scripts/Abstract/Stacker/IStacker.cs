using System.Collections.Generic;
using Abstract.Stack;
using Abstract.Stackable;
using UnityEngine;
using UnityEngine.Events;

namespace Abstract.Stacker
{
    public interface IStacker
    {
        void SetStackHolder(Transform otherTransform);
        void GetStack(GameObject stackableObj);
        void GetAllStack(IStack stack);
        void RemoveStack(IStackable stackable);
        void RemoveAllStack(List<IStackable> stackables);
        void ResetStack(IStackable stackable);
    }
}