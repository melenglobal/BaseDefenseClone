using Abstract.Stackable;
using UnityEngine;

namespace Controllers.StackableControllers
{
    public class StackableGem : AStackable
    {
        public override GameObject SendToStack()
        {
            return transform.gameObject;
        }
    }
}