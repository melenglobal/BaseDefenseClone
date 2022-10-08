using System;
using Abstract;
using Abstract.Interfaces;
using Enums;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class RoomData
    {
        public RoomTypes RoomTypes;
        
        public AvailabilityType AvailabilityType;
        
        public TurretLocationType TurretLocationType;
        
        public TurretData TurretData;
        
        public int PayedAmount;
        
        public int Cost;
    }
}