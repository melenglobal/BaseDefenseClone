using System;
using System.Collections;
using System.Collections.Generic;
using Abstract.Interfaces.Pool;
using AIBrains.EnemyBrain;
using Buyablezone.ConditionHandlers;
using Controllers.AIControllers;
using Controllers.Throw;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Managers.AIManagers
{
    public class EnemySpawnManager : MonoBehaviour,IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables
        
        private List<GameObject> enemyList = new List<GameObject>();

        [SerializeField] 
        private List<Transform> randomTargetTransform;

        [SerializeField] 
        private Transform spawnTransform;

        [SerializeField] 
        private EnemySpawnData enemySpawnData;
        
        [SerializeField]
        private Transform bossSpawnPos;

        [SerializeField]
        private GameObject spriteTarget;

        [SerializeField] 
        private PortalController portalController;

        private const string _dataPath = "Data/CD_AI";

        #endregion

        #region Private Variables
        
        private bool _isSpawning;
        
        #endregion
        #endregion

        private void Awake()
        {
            enemySpawnData = GetData();
        }

        private EnemySpawnData GetData() => Resources.Load<CD_AI>(_dataPath).EnemySpawnData;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {   
            AISignals.Instance.getSpawnTransform += SetSpawnTransform;
            AISignals.Instance.getRandomTransform += SetRandomTransform;
            AISignals.Instance.onReleaseObjectUpdate += OnReleasedObjectCount;
            CoreGameSignals.Instance.onOpenPortal += OnOpenPortal;
        }

        private void UnsubscribeEvents()
        {   
            AISignals.Instance.getSpawnTransform -= SetSpawnTransform;
            AISignals.Instance.getRandomTransform -= SetRandomTransform;
            AISignals.Instance.onReleaseObjectUpdate -= OnReleasedObjectCount;
            CoreGameSignals.Instance.onOpenPortal -= OnOpenPortal;
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
            StartCoroutine(SpawnBoss());
        }
        
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(enemySpawnData.SpawnDelay);
            
            int spawnedEnemies = 0;
            
            _isSpawning = true;
            
            while (spawnedEnemies < enemySpawnData.NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy(); 
                spawnedEnemies++;
                yield return wait;
            }
                
            _isSpawning = false;
        }

        private void OnReleasedObjectCount(GameObject releasedObj)
        {
            enemyList.Remove(releasedObj);
            CheckEnemyCount();
        }

        private void CheckEnemyCount()
        {
            if (enemyList.Count > enemySpawnData.NumberOfEnemiesToSpawn/2) return;
            if (_isSpawning) return;
            StartCoroutine(SpawnEnemies());
        }

        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
            int randomPercentage = Random.Range(0, enemySpawnData.RandomMaxRange);
            
            if (randomType == (int)EnemyType.BigRedEnemy)
            {
                if (randomPercentage< enemySpawnData.MinPossibilityToSpawnEnemy)
                {
                    randomType = (int)EnemyType.RedEnemy;
                }
            }
            
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());
            
            var enemyObj = GetObject(poolType);
            
            enemyList.Add(enemyObj);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return CoreGameSignals.Instance.onGetObjectFromPool(poolName);
        }
        private IEnumerator SpawnBoss()
        {
            yield return new WaitForSeconds(enemySpawnData.SpawnDelay);
            var bossObj = GetObject(PoolType.BossEnemy);
            bossObj.GetComponentInChildren<ThrowEventController>().SpriteTarget = spriteTarget;
            bossObj.transform.position = new Vector3(0f, 0.1f, 175.399994f);
            
        }
        
        private void OnOpenPortal()
        {
            portalController.OpenPortal();
        }

    }
}