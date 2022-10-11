using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MoneyWorkerAIData : Worker
    {
        public Vector3 InitPosition;
        public MoneyWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
        
    }
}