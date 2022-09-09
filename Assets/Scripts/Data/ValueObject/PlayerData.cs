using System;

namespace Data.ValueObject
{   
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
    }
    
    [Serializable]
    public class PlayerMovementData
    {
        public float Speed = 5f;
    }
}