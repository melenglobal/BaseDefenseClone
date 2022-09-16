using System;
using System.CodeDom;
using Enums;

namespace Abstract
{
    public abstract class Buyable
    {
        public int Cost;

        public int PayedAmount;

        protected Buyable(int payedAmount, int cost)
        {
            PayedAmount = payedAmount;
            Cost = cost;
        }
    }
}