using System;
using Data.ValueObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class InitializeDataSignals : MonoSingleton<InitializeDataSignals>
    {   
        public UnityAction<int> onSaveLevelID = delegate(int arg0) {  };
        public UnityAction<BaseRoomData> onSaveBaseRoomData = delegate(BaseRoomData arg0) {  };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate(MineBaseData arg0) {  };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate(MilitaryBaseData arg0) {  };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate(BuyablesData arg0) {  };
       
        
        public UnityAction<int> onLoadLevelID = delegate(int arg0) {  };
        public Func<MilitaryBaseData> onLoadMilitaryBaseData = delegate { return null; };
        public Func<BaseRoomData> onLoadBaseRoomData = delegate { return null; };
        public Func<BuyablesData> onLoadBuyablesData = delegate { return null; };
        public Func<MineBaseData> onLoadMineBaseData = delegate { return null;};
        
        
    }
}