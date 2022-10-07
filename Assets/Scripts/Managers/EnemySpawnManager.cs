using System;
using System.Collections;
using System.Collections.Generic;
using Abstract.Interfaces.Pool;
using AIBrains.EnemyBrain;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour,IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]private List<GameObject> enemies = new List<GameObject>();
        
        #endregion
    
        #region Public Variables
        
        public int NumberOfEnemiesToSpawn = 50;
        
        public float SpawnDelay = 2;

        #endregion

        #region Private Variables
        
        private EnemyType enemyType;
        private PoolType poolType;
        private NavMeshTriangulation triangulation;
        private GameObject _EnemyAIObj;
        private EnemyAIBrain _EnemyAIBrain;
        
        #endregion
        #endregion
        

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }
        
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            
            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }
        }

        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0, Enum.GetNames(typeof(EnemyType)).Length);
            int randomPercentage = Random.Range(0, 101);
            if (randomType == (int)EnemyType.BigRedEnemy)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyType.RedEnemy;
                }
            }
            var poolType = (PoolType)Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());
            GetObject(poolType);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool(poolName);
        }
    }
}