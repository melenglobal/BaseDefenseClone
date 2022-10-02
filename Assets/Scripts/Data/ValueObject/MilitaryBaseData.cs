using System;
using System.Collections.Generic;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MilitaryBaseData : ISaveableEntity
    {
        public Vector2 slotsGrid;

        public Vector2 slotsOffset;
        
        public int MaxSoldierAmount;

        public int CandidateAmount;

        public int CurrentSoldierAmount;

        public int SoldierUpgradeTimer;

        public int SoldierSlotCost;

        public int SlotCount;

        public List<Transform> Slots = new List<Transform>();

        public int AttackTimer;
        
        public string Key = "MilitaryBaseData";

        public string GetKey()
        {
            return Key;
        }
    }
}