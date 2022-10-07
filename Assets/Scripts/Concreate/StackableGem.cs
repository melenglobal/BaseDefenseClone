using Abstract.Stackable;
using UnityEngine;

namespace Concreate
{
    public class StackableGem: AStackable
    {
        public override GameObject SendToStack()
        {
            return transform.gameObject;

        }
    }
}