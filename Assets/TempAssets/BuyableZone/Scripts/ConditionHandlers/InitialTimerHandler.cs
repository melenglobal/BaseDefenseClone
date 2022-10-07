using Buyablezone.Abstract;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Buyablezone.ConditionHandlers
{
    public class InitialTimerHandler:ConditionHandler
    {
        public override void ProcessRequest(PurchaseParam purchase)
            {
                if (purchase.InitialTimer > purchase.InitialTimeOffset)
                {
                    successor.ProcessRequest(purchase);
                }
                else
                {
                    purchase.BuyableZoneManager.StartRadialProgress(purchase.InitialTimer,purchase.InitialTimeOffset);
                    purchase.InitialTimer += Time.deltaTime;
                }
            }
    }
}