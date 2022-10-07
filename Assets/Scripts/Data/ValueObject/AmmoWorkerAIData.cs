﻿using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class AmmoWorkerAIData : Worker
    {
        public Transform WorkerINitTransform;
        public AmmoWorkerAIData(float speed, int capacity) : base(speed, capacity)
        {
        }
    }
}