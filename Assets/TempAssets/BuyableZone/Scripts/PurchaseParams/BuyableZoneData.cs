using System;
using System.Collections.Generic;

namespace Buyablezone.PurchaseParams
{
    [Serializable]
    public class BuyableZoneDataList
    {
        public List<BuyableZoneData> BuyableZoneList=new List<BuyableZoneData>()
        {
            new BuyableZoneData()
        };
        public int BuyableLevel=0;
    }
    [Serializable]
    public class BuyableZoneData
    {
        public int PayedAmount=0;
        public int RequiredAmount=5;
    }
}