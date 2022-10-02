using Buyablezone.Abstract;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Buyablezone.ConditionHandlers
{
    public class CheckCanIncreaseAmountHandler:ConditionHandler
    {
        public override void ProcessRequest(PurchaseParam purchase)
        {
            if (purchase.BuyableZoneManager.IBuyable.MakePayment())
            {
                purchase.BuyableZoneData.PayedAmount++;
                purchase.BuyableZoneManager.UpdateDropzoneText(purchase.BuyableZoneData.RequiredAmount - purchase.BuyableZoneData.PayedAmount);
                successor.ProcessRequest(purchase);
            }
            else
            {
                purchase.BuyableZoneManager.StartPaymentFailed();
            }

        }
    }
}