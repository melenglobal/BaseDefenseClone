using Abstract.Stackable;
using UnityEngine;

namespace Concrete
{
    public class StackableGem : AStackable
    {
        public override GameObject SendToStack()
        {
            return transform.gameObject;
        }
    }
}