using Extentions;
using UnityEngine.Events;
using UnityEngine;
using System;


namespace Signals
{
    public class MoneyWorkerSignals : MonoSingleton<MoneyWorkerSignals>
    {

        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<Transform> onSetMoneyPosition = delegate { };
        public UnityAction <Transform> onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Transform, Transform, Transform> OnMyMoneyTaken = delegate { return null; };
    } 
}
