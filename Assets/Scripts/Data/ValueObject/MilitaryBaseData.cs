using System;
using System.Collections.Generic;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{   
    [Serializable]
    public class MilitaryBaseData : SaveableEntity
    {
        public int MaxSoldierAmount;

        public int CandidateAmount;

        public int CurrentSoldierAmount;

        public int SoldierUpgradeTimer;

        public int SoldierSlotCost;

        public int SlotCount;

        public List<Transform> Slots = new List<Transform>();

        public int AttackTimer;
        
        public string Key = "MilitaryBaseData";
        
        public override string GetKey()
        {
            return Key;
        }
    }
}