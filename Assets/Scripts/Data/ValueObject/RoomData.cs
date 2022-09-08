using System;
using Abstract;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class RoomData :Buyable, ISaveableEntity
    {
        public TurretData TurretData;
        
        public string Key = "RoomData";

        public string GetKey()
        {
            throw new NotImplementedException();
        }

        public RoomData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }
    }
}