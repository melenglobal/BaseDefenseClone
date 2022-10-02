using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Abstract.Stack
{
    public abstract class AStack : MonoBehaviour, IStack
    {
        
        public virtual void SetStackHolder(GameObject gameObject)
        {
            
        }

        public virtual void SetGrid()
        {
            
        }

        public virtual void SendGridDataToStacker()
        {
          
        }
        
    }
}