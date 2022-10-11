using System;
using System.Collections;
using System.Collections.Generic;
using Abstract.Interfaces.Pool;
using AIBrains.EnemyBrain;
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

        [SerializeField] private List<Transform> randomTargetTransform;

        [SerializeField] private Transform spawnTransform;
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

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {   
            AISignals.Instance.getSpawnTransform += SetSpawnTransform;
            AISignals.Instance.getRandomTransform += SetRandomTransform;
        }

        private void UnsubscribeEvents()
        {   
            AISignals.Instance.getSpawnTransform -= SetSpawnTransform;
            AISignals.Instance.getRandomTransform -= SetRandomTransform;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private Transform SetSpawnTransform()
        {
            return spawnTransform;
        }

        private Transform SetRandomTransform()
        {
            return randomTargetTransform[Random.Range(0,randomTargetTransform.Count)];
        }
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
            int randomType = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
            int randomPercentage = Random.Range(0, 101);
            if (randomType == (int)EnemyType.BigRedEnemy)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyType.RedEnemy;
                }
            }
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());
            GetObject(poolType);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool(poolName);
        }
    }
}