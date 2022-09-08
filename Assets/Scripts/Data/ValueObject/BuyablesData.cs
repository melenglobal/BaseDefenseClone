using System;
using Abstract;
using Enums;

namespace Data.ValueObject
{   
    [Serializable]
    public class BuyablesData
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayedAmount;
        
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;  //Workers

        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;

        public AvailabilityType AvailabilityType;
        
    }
}