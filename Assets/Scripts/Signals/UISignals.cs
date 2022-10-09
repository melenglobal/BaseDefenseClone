using System.Collections;
using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction<UIPanels> onOpenPanel = delegate { };
    public UnityAction<UIPanels> onClosePanel = delegate { };
    public UnityAction<int> onSetLevelText=delegate { };
    public UnityAction<int> onUpdateMoneyScore = delegate(int arg0) {  };
    public UnityAction<int> onUpdateGemScore = delegate(int arg0) {  };
}
