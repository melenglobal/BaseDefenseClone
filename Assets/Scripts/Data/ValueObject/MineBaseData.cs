using System;
using Abstract;

namespace Data.ValueObject
{   
    [Serializable]
    public class MineBaseData : SaveableEntity
    {
        public int MaxWorkerAmount;
        
        public int CurrentWorkerAmount;
        
        public int DiamondCapacity;
        
        public int CurrentDiamondAmount;
        
        public int MineCardCapacity;
        
        public string Key = "MineBaseData";
        public override string GetKey()
        {
            return Key;
        }
    }
}