using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{  
    [Serializable]
    public class StageData : IBuyable
    {
        public int Cost { get; set; }
        
        public int PayedAmount { get; set; }
        
        public AvailabilityType AvailabilityType;
    }
}