using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel = delegate { };
        public UnityAction<UIPanels> onClosePanel = delegate { };
        public UnityAction<int> onSetLevelText=delegate { };
        public UnityAction<int> onUpdateMoneyScore = delegate(int arg0) {  };
        public UnityAction<int> onUpdateGemScore = delegate(int arg0) {  };
        public UnityAction onOutDoorHealthOpen = delegate {  };
        public UnityAction onHealthVisualClose = delegate {  };
        public UnityAction<int> onHealthUpdate = delegate(int arg0) {  };
    }
}
