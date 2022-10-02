using Managers;

namespace Buyablezone.PurchaseParams
{
    public class PurchaseParam
    {
            public bool isInputActive;
            public bool alreadyBuyed;
            public bool isCoolDownCompleted=true;
            public int CurrentBuyableLevel;
            public float InitialTimer;
            public float OffsetTimer;
            public float InitialTimeOffset;
            public float TimeOffset;
            public BuyableZoneManager BuyableZoneManager;
            public BuyableZoneData BuyableZoneData;
            public BuyableZoneDataList BuyableZoneList;

            // Constructor
            public PurchaseParam(BuyableZoneDataList buyableZoneList, float timeOffset, float ınitialTimeOffset,BuyableZoneManager buyableZoneManager)
            {
                this.BuyableZoneManager = buyableZoneManager;
                this.BuyableZoneList = buyableZoneList;
                this.InitialTimeOffset = ınitialTimeOffset;
                this.TimeOffset = timeOffset;//factory pattern Composite pattern
                CurrentBuyableLevel = BuyableZoneList.BuyableLevel;//gereksiz olabilir
                BuyableZoneData = buyableZoneList.BuyableZoneList[buyableZoneList.BuyableLevel];
            }
            public void IncreaseBuyableZoneLevel()
            {
                BuyableZoneList.BuyableLevel++;
                BuyableZoneList.BuyableLevel=BuyableZoneList.BuyableLevel % BuyableZoneList.BuyableZoneList.Count;
                CurrentBuyableLevel=BuyableZoneList.BuyableLevel;
                BuyableZoneData = BuyableZoneList.BuyableZoneList[BuyableZoneList.BuyableLevel];
            }
            
    }
}