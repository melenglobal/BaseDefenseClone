using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
       public UnityAction<RoomTypes> onChangeExtentionVisibility = delegate(RoomTypes arg0) {  }; 
    }
}