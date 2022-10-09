using System;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
       public UnityAction<RoomTypes> onChangeExtentionVisibility = delegate(RoomTypes arg0) {  }; 
       public UnityAction<RoomTypes,int> onTakePayment =delegate(RoomTypes arg0, int i) {  };
       public Func<RoomTypes, int> onUpdateRoomCostText;
    }
}