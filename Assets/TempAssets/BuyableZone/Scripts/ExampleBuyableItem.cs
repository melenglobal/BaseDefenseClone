using System.Collections;
using System.Collections.Generic;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using UnityEngine;

public class ExampleBuyableItem : MonoBehaviour,IBuyable
{
    BuyableZoneDataList BuyableZoneDataList = new BuyableZoneDataList();
    public BuyableZoneDataList GetBuyableData()
    {
       
        return BuyableZoneDataList;
    }

    public void TriggerBuyingEvent()
    {
        Debug.Log("triggerlandi");
    }

    public bool MakePayment()
    {
        return true;
            
    }
}
