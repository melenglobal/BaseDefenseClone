using System;

namespace Data.ValueObject
{   
    [Serializable]
    public class LevelData
    {
        public FrondYardData FrondYardData;
        public BaseData BaseData;

        public int TotalGemScore;
        
        public int TotalMoneyScore;

    }
}