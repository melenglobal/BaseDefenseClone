using System;
using Abstract;
using Abstract.AbstractClasses;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MoneyWorkerAIData : Worker
    {
        public int Cost;
        public MoneyWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
        
    }
}