using System;
using Abstract;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class TurretData : Buyable,ISaveableEntity
    {
        public AvailabilityType AvailabilityType;

        public bool HasTurretSoldier;

        public int AmmoCapacity;

        public int AmmoDamage;

        public ParticleSystem TurretParticle;
        
        public string Key = "TurretData";
        
        public TurretData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }

        public string GetKey()
        {
            return Key;
        }
    }
}