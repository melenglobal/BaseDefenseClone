using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DropzoneSignals : MonoSingleton<DropzoneSignals>
    {
        public UnityAction<bool> onDropZoneFull=delegate{};
    }
}