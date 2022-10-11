using System;
using Enums;
using Extentions;
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
        
        public UnityAction onSetCameraTarget = delegate { };
        
        public UnityAction onApplicationPause = delegate { };
        
        public UnityAction onApplicationQuit = delegate { };
        
        public Func<PoolType,GameObject> onGetObjectFromPool = delegate { return null;};
        
        public UnityAction<PoolType,GameObject> onReleaseObjectFromPool = delegate {  };

        public UnityAction<int> onUpdateGemScoreData = delegate(int arg0) {  };
        
        public UnityAction<int> onUpdateMoneyScoreData = delegate(int arg0) {  };
        
        public UnityAction<InputHandlers> onInputHandlerChange = delegate(InputHandlers arg0) {  };

        public UnityAction onCharacterInputRelease = delegate {  };

        public UnityAction<TurretLocationType,GameObject> onSetCurrentTurret = delegate(TurretLocationType arg0, GameObject o) {  };

        public Func<bool> onHasEnoughMoney;

        public Func<bool> onHasEnoughGem;
        
        public UnityAction onStartMoneyPayment = delegate {  };
        
        public UnityAction onStopMoneyPayment = delegate { };
        
        public UnityAction onEnterTurret = delegate {  };
        
        public UnityAction onLevel = delegate {  };
        
        public UnityAction onFinish = delegate {  };


    }
}
