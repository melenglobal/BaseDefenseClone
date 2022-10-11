using System;
using System.Collections.Generic;

namespace Data.ValueObject
{   
    [Serializable]
    public class FrondYardData
    {
        public List<StageData> StageDatas;
        public List<FrondYardItemsData> FrondYardItemsDatas;
    }
}