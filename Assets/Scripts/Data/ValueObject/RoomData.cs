using System;
using Abstract;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class RoomData : SaveableEntity,IBuyable
    {
        public TurretData TurretData;
        
        public string Key = "RoomData";
        
        public override string GetKey()
        {
            return Key;
        }
        
        public int Cost { get; set; }
        
        public int PayedAmount { get; set; }
        
    }
}