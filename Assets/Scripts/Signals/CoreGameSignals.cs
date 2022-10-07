using System;
using Controllers;
using Enums;
using Extentions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals :MonoSingleton<CoreGameSignals>
    {

        public UnityAction onLevelInitialize = delegate { };
        
        public UnityAction onIdleLevelInitialize = delegate { };
        
        public UnityAction onClearActiveLevel = delegate { };
        
        public UnityAction onClearActiveIdleLevel = delegate { };
        
        public UnityAction onFailed = delegate { };

        public UnityAction onNextLevel = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onSetCameraTarget = delegate { };
        
        public UnityAction onApplicationPause = delegate { };
        
        public UnityAction onApplicationQuit = delegate { };

        //public Func<int> onGetLevelID = delegate { return 0; };
        
        public Func<int> onGetIdleLevelID = delegate { return 0; };
        
        public UnityAction onIdleLevelChange = delegate { };
    
        public Func<PoolType,GameObject> onGetObjectFromPool = delegate { return null;};
        
        public UnityAction<PoolType,GameObject> onReleaseObjectFromPool = delegate {  };

        public UnityAction<int> onUpdateGemScore = delegate(int arg0) {  };
        
        public UnityAction<int> onUpdateMoneyScore = delegate(int arg0) {  };
        
        public UnityAction<InputHandlers> onInputHandlerChange = delegate(InputHandlers arg0) {  };

        public UnityAction onCharacterInputRelease = delegate {  };

        public UnityAction<TurretLocationType> onSetCurrentTurret = delegate(TurretLocationType arg0) {  };
  
    }
}
