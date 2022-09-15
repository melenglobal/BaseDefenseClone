using System.Collections.Generic;
using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Search : IState
    {   
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _spawnPosition;
        private readonly List<Transform> _targetList;

        public Search(EnemyAIBrain enemyAIBrain,NavMeshAgent navmeshAgent,Transform spawnPosition)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = navmeshAgent;
            _spawnPosition = spawnPosition;
        }
        

        public void Tick()
        {
            
        }

        public void OnEnter()
        { 
            _navMeshAgent.enabled = true;
            

            GetRandomPointOnBakedSurface();

        }

        public void OnExit()
        {
            
        }
        
        private void GetRandomPointOnBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * range;
                    Vector3 randomPos = new Vector3(randomPoint.x, 0, _spawnPosition.transform.position.z);
                    NavMeshHit hit;
                    if (!NavMesh.SamplePosition(randomPos, out hit, 1.0f, 1)) continue;
                    result = hit.position;
                    return true;

                }
                result = Vector3.zero;
                return false;

            }

            if (!RandomPoint(_spawnPosition.position, 20, out var point)) return;
            _navMeshAgent.Warp(point);
        }
        
        
    }
}