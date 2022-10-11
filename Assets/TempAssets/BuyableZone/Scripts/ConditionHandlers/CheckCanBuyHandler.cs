using Buyablezone.Abstract;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Buyablezone.ConditionHandlers
{
    public class CheckCanBuyHandler:ConditionHandler
    {
        private BuyableZoneData BuyableZoneData; 
        public override void ProcessRequest(PurchaseParam purchase)
        {
            BuyableZoneData = purchase.BuyableZoneList.BuyableZoneList[purchase.CurrentBuyableLevel];
            if (BuyableZoneData.PayedAmount >= BuyableZoneData.RequiredAmount)
            {

                purchase.BuyableZoneManager.StartPurchaseConfiguration(purchase);

            }
        }

       
    }
}