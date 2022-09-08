using System;
using System.Collections.Generic;

namespace Data.ValueObject
{   
    [Serializable]
    public class BaseRoomData
    {
        public List<RoomData> RoomDatas = new List<RoomData>();
    }
}