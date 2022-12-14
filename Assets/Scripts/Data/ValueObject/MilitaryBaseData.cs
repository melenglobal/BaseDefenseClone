using System;
using System.Collections.Generic;
using Abstract;
using Abstract.Interfaces;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MilitaryBaseData : ISaveableEntity
    {   
        public int BaseCapacity;

        public int TentCapacity;
        
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