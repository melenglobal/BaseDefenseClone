using System;
using Abstract;
using Abstract.AbstractClasses;

namespace Data.ValueObject
{   
    [Serializable]
    public class MineWorkerAIData : Worker
    {
        public MineWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
        
    }
}