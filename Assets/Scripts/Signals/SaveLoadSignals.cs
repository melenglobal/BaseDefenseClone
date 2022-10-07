using UnityEngine.Events;
using System;
using Data.UnityObject;
using Extentions;


namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<CD_Level,int> onSaveGameData = delegate { };
        
        public Func<string, int, CD_Level> onLoadGameData;
    }
}