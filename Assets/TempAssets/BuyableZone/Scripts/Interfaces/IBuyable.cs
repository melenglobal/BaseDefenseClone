using Buyablezone.PurchaseParams;

namespace Buyablezone.Interfaces
{
    public interface IBuyable
        {
            /// <summary>
            /// GetBuyableData(): Required Amount have to return in here
            /// TriggerBuyingEvent(): This function Will trigger on buyablezone completed
            ///  MakePayment(): Payment have to Handle in interface instance func ,it returns Pay statements succes or not legible states as a bool
            /// </summary>    
            public BuyableZoneDataList GetBuyableData();
            
            public void TriggerBuyingEvent();
            public bool MakePayment();
           
        }
}