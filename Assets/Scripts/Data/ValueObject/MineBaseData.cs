using System;
using Abstract;
using Abstract.Interfaces;

namespace Data.ValueObject
{   
    [Serializable]
    public class MineBaseData
    {
        public int MaxWorkerAmount;
        
        public int CurrentWorkerAmount;

        public int GemCollectionOffset;

    }
}