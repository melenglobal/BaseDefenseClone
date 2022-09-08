using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class MoneyWorkerAIData : IWorker
    {
        public int Capacity { get; set; }
        
        public float Speed { get; set; }
    }
}