using System;
using System.CodeDom;
using Abstract.Interfaces;
using Enums;

namespace Abstract
{
    public abstract class Buyable
    {
        private int Cost;

        private int PayedAmount;
        
        protected Buyable(int payedAmount, int cost)
        {
            PayedAmount = payedAmount;
            Cost = cost;
        }
        
    }
}