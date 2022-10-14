using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;

namespace Signals
{
    public class AISignals : MonoSingleton<AISignals>
    {
        public UnityAction onSoldierActivation = delegate {  };

        public Func<Transform> getSpawnTransform;

        public Func<Transform> getRandomTransform;

        public UnityAction<GameObject> onReleaseObjectUpdate = delegate(GameObject arg0) {  };
        
        public UnityAction onSoldierAmountUpgrade  =delegate {  };
    }
}