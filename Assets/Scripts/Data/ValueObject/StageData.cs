using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{  
    [Serializable]
    public class StageData : Buyable
    {
        
        public AvailabilityType AvailabilityType;
        public string Stages = "Stage";
        public StageData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }
        
    }
}