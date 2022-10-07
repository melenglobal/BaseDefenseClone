using AI.MinerAI;
using Extentions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class HostageSignals:MonoSingleton<HostageSignals>
    {
        public UnityAction<MinerManager> onAddHostageStack=delegate{};
        public UnityAction<Vector3> onClearHostageStack=delegate {  };
    }
}