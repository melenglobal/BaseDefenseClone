using System;
using Abstract;
using Abstract.Interfaces;
using Enums;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class RoomData :Buyable
    {
        public TurretLocationType TurretLocationType;
        
        public TurretData TurretData; 
        
        public RoomData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }
    }
}