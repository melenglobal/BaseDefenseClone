using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction onEnableInput = delegate {  };
    public UnityAction onDisableInput = delegate {  };
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction<InputParams> onJoyStickInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };
}
