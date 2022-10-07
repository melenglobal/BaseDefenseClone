using Buyablezone.Abstract;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Buyablezone.ConditionHandlers
{
    public class CheckPayOffsetHandler:ConditionHandler
    {
        public override void ProcessRequest(PurchaseParam purchase)
            {
                if (purchase.OffsetTimer > purchase.TimeOffset)
                {
                    successor.ProcessRequest(purchase);
                    purchase.OffsetTimer = 0;
                
                }
                else
                {
                    purchase.OffsetTimer += Time.deltaTime;
                }
            }
    }
}