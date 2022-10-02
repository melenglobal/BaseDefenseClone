using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
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
        private NavMeshTriangulation triangulation;
        private GameObject _EnemyAIObj;
        private EnemyAIBrain _EnemyAIBrain;
        
        #endregion
        #endregion
        

        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                ObjectPoolManager.Instance.AddObjectPool(()=>Instantiate(enemies[i]),TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
            }
            
        }

        private void Awake()
        {   
            InitEnemyPool();
            
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private void TurnOnEnemyAI(GameObject enemy)
        {
            enemy.SetActive(true);
        }

        private void TurnOffEnemyAI(GameObject enemy)
        {
            enemy.SetActive(false);
        }
        private void ReleaseEnemyObject(GameObject go,Type t)
        {
            ObjectPoolManager.Instance.ReturnObject(go,t.ToString());
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
            if (randomType == (int)EnemyType.BigRed)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyType.Red;
                    Debug.Log(randomType);
                }
            }
            ObjectPoolManager.Instance.GetObject<GameObject>(((EnemyType)randomType).ToString());
        }
    }
}