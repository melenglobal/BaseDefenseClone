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
        
        [SerializeField]private List<Transform> targetList = new List<Transform>();
        

        #endregion
    
        #region Public Variables
        
        public int NumberOfEnemiesToSpawn = 50;
        
        public float SpawnDelay = 2;

        public Transform SpawnPos;

        #endregion

        #region Private Variables
        
        private EnemyType enemyType;
        
        private List<EnemyAIBrain> enemyScripts = new List<EnemyAIBrain>();
        
        private NavMeshTriangulation triangulation;
        
        private GameObject _EnemyAIObj;
        private EnemyAIBrain _EnemyAIBrain;


        

        #endregion
        #endregion
        

        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
            {   
                enemies[i].GetComponent<EnemyAIBrain>()._currentTarget =targetList[Random.Range(0,targetList.Count)];
                ObjectPoolManager.Instance.AddObjectPool(()=>Instantiate(enemies[i]),TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
            }
            
        }

        private void Awake()
        {   
            InitEnemyPool();

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
            _EnemyAIObj = ObjectPoolManager.Instance.GetObject<GameObject>(((EnemyType)randomType).ToString());
            _EnemyAIBrain = _EnemyAIObj.GetComponent<EnemyAIBrain>();
            
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
           {
               for (int i = 0; i < 60; i++)
               {
                   Vector3 randomPoint = center + Random.insideUnitSphere * range;
                   Vector3 randomPos = new Vector3(randomPoint.x, 0, SpawnPos.transform.position.z);
                   NavMeshHit hit;
                   if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, 1))
                   {
                       result = hit.position;
                       return true;
                   }
                   
               }
               result = Vector3.zero;
               return false;

           }

           Vector3 point;
           if (!RandomPoint(SpawnPos.position, 20, out point)) return;
           
           _EnemyAIBrain.navMeshAgent.Warp(point);
           

        }
    }
}