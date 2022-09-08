using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class MoneyWorkerAIData : Worker
    {
        public MoneyWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
        
    }
}