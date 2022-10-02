using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour,ISaveable
    {

        #region Self Variables

        #region Public Variables

        public bool IsBaseAvailable { get; set; }
        public bool IsTentAvailable { get; set; }
        
        public int BaseCapacity;

        public int TotalAmount;
        
        public int TentCapacity;
        
        public int SoldierAmount;

        public int ProcessTimer;
        
        public float MaxTime = 3.0f;
        
        public float Timer = 0.0f;
        
        public GameObject SlotPrefab;

        public GameObject ZoneParent;
        
        public List<Transform> Slots = new List<Transform>();

        public bool hasTarget;

        #endregion

        #region Serialized Variables

        [SerializeField] private MilitaryBaseData militaryBaseData;
        
        [SerializeField]
        private Transform waitZoneArea;

        #endregion

        #region Private Variables

        

        // filledamount yaz 

        #endregion
        

        #endregion


        private void Awake()
        {
            militaryBaseData = GetBaseData();
            SetWaitSlots();
        }

        private MilitaryBaseData GetBaseData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].BaseData.MilitaryBaseData;
        
        private void SetWaitSlots()
        {
            int gridX = (int)militaryBaseData.slotsGrid.x;
            int gridY = (int)militaryBaseData.slotsGrid.y;

            var localScale = ZoneParent.transform.localScale;
            Vector3 parentPivot = new Vector3(localScale.x / 2, 0,
                localScale.z / 2);
            for (int i = 0; i < gridX; i++)
            {   
                for (int j = 0; j < gridY; j++)
                {
                    Vector3 slotPos = new Vector3(i * militaryBaseData.slotsOffset.x, 1,
                        j * militaryBaseData.slotsOffset.y);
                    Instantiate(SlotPrefab, ZoneParent.transform.localPosition + parentPivot + slotPos,
                   Quaternion.identity,ZoneParent.transform);
                }
            }
        }

        public void UpdateTotalAmount(int amount)
        {
            if (!IsBaseAvailable)
                return;
            
            if (TotalAmount < BaseCapacity)
            {
                TotalAmount += amount;
                //UpgradeText
            }
            else
            {
                IsBaseAvailable = false;
            }
        }
        
        //TentCapacity
        public void UpdateSoldierAmount(int amount)
        {
            if (!IsTentAvailable)
                return;
            
            if (SoldierAmount < TentCapacity)
            {
                SoldierAmount += amount;
            }
            else
            {
                IsTentAvailable = false;
            }
        }
        
        private void Attack() // Tetikle
        {
            TotalAmount -= SoldierAmount;

            SoldierAmount = 0;
        }
        
        private IEnumerator CountDown() // Eleman girdiginde startcoroutine, // Eleman ciktiginda stopcoroutine
        {
            
            while (Timer<=1f)
            {   
                
                Timer += Time.deltaTime / MaxTime;
                //Imagefill
                yield return null;
            }
            
        }

        public void GoTarget(Transform currentTransform,Transform nextTransform)
        {
            currentTransform.DOMoveZ(nextTransform.position.z, 1f);
        }
        // State i Attack olduktan sonra baslat.
        //Transform candidate position,transform soldier position,
        // İlk triggerlandı hostages,nereye gidiyor?
        // Gittigi yerden tent icerisinde bir pozisyona yuruyor
        // tent icerisinden cesitli transform pointlerimize yuruyor
        // attack e basildiktan sonra base disina hareket ediyorlar.

        public void Save(int uniqueId)
        {
            
        }

        public void Load(int uniqueId)
        {
            
        }
    }
}