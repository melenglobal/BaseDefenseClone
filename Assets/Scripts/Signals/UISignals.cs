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
    public UnityAction onMultiplyArea=delegate { };
    public UnityAction<int> onSetLevelText=delegate { };
}
