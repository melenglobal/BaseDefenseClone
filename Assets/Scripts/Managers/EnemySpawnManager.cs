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
        public Transform Player;
        public int NumberOfEnemiesToSpawn = 50;
        
        public float SpawnDelay = 2;

        [SerializeField]private List<GameObject> enemies = new List<GameObject>();

        private EnemyType enemyType;
        
        private List<EnemyAIBrain> enemyScripts = new List<EnemyAIBrain>();
        
        public GameObject EnemyPrefab;

        public SpawnMethod EnemySpawnMethod;

        private NavMeshTriangulation triangulation;

        public Transform SpawnPos;

        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i == 0)
                {   
                    Debug.Log(((EnemyType)i).ToString());
                    ObjectPoolManager.Instance.AddObjectPool(RedEnemyFactoryMethod,TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
                }
                // else if (i == 1)
                // {
                //     ObjectPoolManager.Instance.AddObjectPool(OrangeEnemyFactoryMethod,TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
                // }
                // else
                // {
                //     ObjectPoolManager.Instance.AddObjectPool(BigRedEnemyFactoryMethod,TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
                // }
                
                
            }
            
        }
        private void Awake()
        {
            
        }

        private void Start()
        {   
            InitEnemyPool();
            
            triangulation = NavMesh.CalculateTriangulation();
            
            StartCoroutine(SpawnEnemies());
        }
        
        private GameObject RedEnemyFactoryMethod()
        {
            return  Instantiate(enemies[0]);
        }

        private GameObject OrangeEnemyFactoryMethod()
        {
            return Instantiate(enemies[1]);
        }
        private GameObject BigRedEnemyFactoryMethod()
        {
            return Instantiate(enemies[2]);
        }

        private void TurnOnEnemyAI(GameObject enemy)
        {
            enemy.SetActive(true);
        }

        private void TurnOffEnemyAI(GameObject enemy)
        {
            enemy.SetActive(false);
            
        }
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            
            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                if (EnemySpawnMethod == SpawnMethod.RoundRobin)
                {
                    SpawnRoundRobinEnemy(spawnedEnemies);
                }

                spawnedEnemies++;
                yield return wait;
            }
        }

        private void SpawnRoundRobinEnemy(int spawnedEnemies)
        {
            DoSpawnEnemy();
        }

        private void SpawnRandomEnemy()
        {
            DoSpawnEnemy();
        }
        
        private void ReleaseEnemyObject(GameObject go,Type t)
        {
            ObjectPoolManager.Instance.ReturnObject(go,t.ToString());
        }
        private void DoSpawnEnemy()
        {
           
           GameObject enemyAI = ObjectPoolManager.Instance.GetObject<GameObject>("Orange");
           
           EnemyAIBrain brain = enemyAI.GetComponent<EnemyAIBrain>();

           int vertexIndex = Random.Range(0,triangulation.vertices.Length);
           
           Debug.Log(triangulation.vertices);
           
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
           if (RandomPoint(SpawnPos.position,20 ,out point))
           {   
               brain.navMeshAgent.Warp(point);
             
           }
           // if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex],out hit,2f,1)) // 1 for walkable
           // {
           //     
           //  brain.navMeshAgent.Warp(hit.position);
           //  //brain.target = Player;
           //  brain.navMeshAgent.enabled = true; // Target may be different
           //  //enemyAI.StartChasing();
           //
           // }
           else
           {
               Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {triangulation.vertices[vertexIndex]}");
           }
          
        }
        public enum SpawnMethod
        {
            RoundRobin,
            Random
        }
    }
}