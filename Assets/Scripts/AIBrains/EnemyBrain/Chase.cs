﻿using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Chase : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        public bool isPlayerInRange; // check distance agent.remaining
        public Chase(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            _navMeshAgent.SetDestination(_enemyAIBrain.target.position);
        }

        public void OnEnter()
        {
            Debug.Log("DEBUG CHASE!");
            _navMeshAgent.enabled = true;
        }

        public void OnExit()
        {
            //kosma
        }
    }
}