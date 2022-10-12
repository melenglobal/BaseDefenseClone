using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {   
        
        public UnityAction<InputParams> onJoystickInputDragged = delegate { };
        
        public UnityAction<InputParams> onJoystickInputDraggedforTurret = delegate(InputParams arg0) {  };
        
        public UnityAction<InputHandlers> onInputHandlerChange = delegate(InputHandlers arg0) {  };
        
        public UnityAction onCharacterInputRelease = delegate {  };
        


    }
}
