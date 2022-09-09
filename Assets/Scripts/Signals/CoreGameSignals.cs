using System;
using System.Collections;
using System.Collections.Generic;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals :MonoSingleton<CoreGameSignals>
{
    public UnityAction onEnterMutiplyArea= delegate { };

    public UnityAction onEnterIdleArea = delegate { };
       
   // public UnityAction <GameStates> onChangeGameState = delegate { };
        
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
}
