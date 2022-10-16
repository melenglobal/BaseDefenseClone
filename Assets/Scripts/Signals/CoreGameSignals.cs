using System;
using Controllers.StackableControllers;
using Enums;
using Extentions;
using Managers.CoreGameManagers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {

        public UnityAction onLevelInitialize = delegate { };

        public UnityAction onClearActiveLevel = delegate { };

        public UnityAction onFailed = delegate { };

        public UnityAction onNextLevel = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onResetPlayer = delegate {  };

        public UnityAction<Transform> onSetCameraTarget = delegate { };
        
        public UnityAction onApplicationPause = delegate { };
        
        public UnityAction onApplicationQuit = delegate { };
        
        public Func<PoolType,GameObject> onGetObjectFromPool = delegate { return null;};
        
        public UnityAction<PoolType,GameObject> onReleaseObjectFromPool = delegate {  };

        public UnityAction onUpdateGemScoreData = delegate {  };
        
        public UnityAction onUpdateMoneyScoreData = delegate {  };

        public UnityAction<TurretLocationType,GameObject> onSetCurrentTurret = delegate(TurretLocationType arg0, GameObject o) {  };

        public Func<bool> onHasEnoughMoney = delegate { return default; };

        public Func<bool> onHasEnoughGem = delegate { return default; };
        
        public UnityAction onStartMoneyPayment = delegate {  };
        
        public UnityAction onStopMoneyPayment = delegate { };
        
        public UnityAction onEnterTurret = delegate {  };
        
        public UnityAction onLevel = delegate {  };
        
        public UnityAction onFinish = delegate {  };

        public Func<int> onGetHealthValue = delegate { return default; };
        
        public UnityAction<int> onTakePlayerDamage = delegate(int arg0) {  };
        
        public UnityAction<Transform> onPlayerInitialize = delegate(Transform arg0) {  };
        
        public UnityAction onOpenPortal = delegate { };
        
        public UnityAction<StackableMoney> onMoneyReleased  = delegate(StackableMoney arg0) {  };
    }
}
