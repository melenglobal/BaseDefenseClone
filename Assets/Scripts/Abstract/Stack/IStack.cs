using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Abstract.Stack
{
    public interface IStack
    {
        void SetStackHolder(GameObject moneyHolder);
        void SetGrid();
        void SendGridDataToStacker();
        
    }
}