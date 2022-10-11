using System;
using System.Collections.Generic;
using Abstract;
using Abstract.Interfaces;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class BuyablesData : ISaveableEntity
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayedAmount;
        
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;  //Workers

        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;
        
        public AvailabilityType AvailabilityType;


        public string GetKey()
        {
            throw new NotImplementedException();
        }
    }
}