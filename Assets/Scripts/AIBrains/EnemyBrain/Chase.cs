using Abstract;
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
            _navMeshAgent.destination = _enemyAIBrain._currentTarget.position;
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_enemyAIBrain._currentTarget.position);
            //_navMeshAgent.enabled = true;
            Debug.Log(_enemyAIBrain._currentTarget.name);
        }

        public void OnExit()
        {
            //kosma
        }
    }
}