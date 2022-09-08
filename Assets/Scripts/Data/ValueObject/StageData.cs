using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{  
    [Serializable]
    public class StageData : Buyable
    {
        
        public AvailabilityType AvailabilityType;

        public StageData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }
    }
}