using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MainRoomData
    {
        public TurretLocationType TurretLocationType;
        
        public TurretData TurretData;
    }
}