using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class AmmoWorkerAIData : IWorker
    {
        public int Capacity { get; set; }
        
        public float Speed { get; set; }
    }
}