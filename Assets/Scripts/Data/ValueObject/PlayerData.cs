using System;

namespace Data.ValueObject
{   
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;

        public int Health;
    }
    
    [Serializable]
    public class PlayerMovementData
    {
        public float Speed = 20f;
        
        public float ExitClampLeftSide = -0.3f;

        public float ExitClampRightSide = +0.3f;

        public float ExitClampBackSide = -0.6f;
        
    }
}