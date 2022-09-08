using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class AmmoWorkerAIData : Worker
    {
        public AmmoWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
    }
}