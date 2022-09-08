using System;
using Abstract;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class TurretData : SaveableEntity,IBuyable
    {
        public AvailabilityType AvailabilityType;

        public bool HasTurretSoldier;

        public int AmmoCapacity;

        public int AmmoDamage;

        public ParticleSystem TurretParticle;
        
        public string Key = "TurretData";
        
        public override string GetKey()
        {
            return Key;
        }

        public int Cost { get; set; }
        
        public int PayedAmount { get; set; }
    }
}