using System;
using Abstract;
using Abstract.Interfaces;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data.ValueObject
{   
    [Serializable]
    public class TurretData : Buyable
    {
        public AvailabilityType AvailabilityType; // Player etkileşime geçebilir mi geçemez mi diye

        public bool HasTurretSoldier; // Has turret soldier savelenecek data

        public int AmmoCapacity;  // Maksimum tasiyabilecegi Ammo miktrari

        public int AmmoDamage;  // bir Ammonun verdigi damage miktari

        public ParticleSystem TurretParticle;

        public TurretData(int payedAmount, int cost) : base(payedAmount, cost)
        {
        }

    }
}