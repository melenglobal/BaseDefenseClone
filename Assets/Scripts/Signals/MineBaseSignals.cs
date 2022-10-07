using System;
using Enum;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class MineBaseSignals:MonoSingleton<MineBaseSignals>
    {
        public Func<Tuple<Transform,GemMineType>> onGetRandomMineTarget= delegate { return null;};
        public Func<Transform> onGetGemHolderPos= delegate { return null;};
    }
}