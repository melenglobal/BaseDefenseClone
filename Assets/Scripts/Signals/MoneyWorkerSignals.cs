using System;
using Controllers.StackableControllers;
using Data.ValueObject;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MoneyWorkerSignals : MonoSingleton<MoneyWorkerSignals>
    {
        public Func<MoneyWorkerAIData> onGetMoneyAIData = delegate { return default; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<StackableMoney> onSetStackable = delegate { };
        public UnityAction onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Vector3> onSendWaitPosition = delegate { return Vector3.zero; };
    
    } 
}
