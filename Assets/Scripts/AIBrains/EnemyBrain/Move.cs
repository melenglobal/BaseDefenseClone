using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Move : IState
    {   
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private Vector3 _lastPosition = Vector3.zero;
        
        public float TimeStuck;
        public Move(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {   
            Debug.Log("Tick is updating!");
            if (Vector3.Distance(_enemyAIBrain.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;
            
            _lastPosition = _enemyAIBrain.transform.position;
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.target.transform.position);
            //_animator.SetFloat(Speed, 1f);
        }

        public void OnExit()
        {
             _navMeshAgent.enabled = false;
            // _animator.SetFloat(Speed, 0f);
        }
    }
}