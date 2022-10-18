using System;
using Abstract.Stackable;
using Enums;
using Signals;
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