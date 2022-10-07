using Data.ValueObject;
using Signals;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

        

    #endregion

    #region Serialized Variables

        

    #endregion

    #region Private Variables

    public BuyablesData _data;

    #endregion

    #endregion


    #region Event Subscribetions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InitializeDataSignals.Instance.onLoadBuyablesData += OnLoadData;
    }

    private void UnsubscribeEvents()
    {
        InitializeDataSignals.Instance.onLoadBuyablesData -= OnLoadData;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void OnLoadData(BuyablesData mineBaseData)
    {
        _data = mineBaseData;
    }
}
