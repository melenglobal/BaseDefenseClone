using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AmmoManagerSignals : MonoSingleton<AmmoManagerSignals>
    {
        public UnityAction <GameObject> onSetConteynerList = delegate { };
        public UnityAction <Transform> onPlayerEnterAmmoWorkerCreaterArea = delegate { };
    }
}