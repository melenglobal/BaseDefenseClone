using System;
using System.Linq;
using AIBrains.EnemyBrain;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class TimeBomb : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        private FrondYardItemsData frondYardItemsData;
        private EnemyAIBrain enemyAIBrain;
        private float ExplosionTimer = 7;
        
        private float BombReadyTimer = 60;
        
        private bool isBombActive;

        private bool isBombSetup;

        private int pickFromNearest;

        #endregion


        #endregion

        private void Awake()
        {
            frondYardItemsData = GetData();
            
        }


        private FrondYardItemsData GetData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrondYardData
            .FrondYardItemsDatas[0];

        private void FixedUpdate()
        {
            if (!isBombSetup) return;
            if (isBombActive)
            {
                if (ExplosionTimer > 0)
                {
                    if (ExplosionTimer >= 9.7)
                    {
                        FindEnemyInRange().BombSettled(this.transform); 
                    }
                    
                    ExplosionTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    ExplosionTimer = 10;
                    //Play Particle
                    isBombActive = false;
                }
            }
            else
            {
                if (BombReadyTimer > 0)
                {
                    BombReadyTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    BombReadyTimer = 60;
                    
                    isBombActive = true;
                        
                    isBombSetup = false;
                }
            }
            
        }
        
        private EnemyAIBrain FindEnemyInRange()
        {
            return Object.FindObjectsOfType<EnemyAIBrain>()
                .Where(t=> Vector3.Distance(transform.position, t.transform.position) < 25)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {   
                isBombSetup = true;
                
            }
   
        }
        
        
    }
}