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
        private Vector3 _lastPosition = Vector3.zero;

        private float timeStuck;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Run = Animator.StringToHash("Run");

        public Move(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {   
            if (Vector3.Distance(_enemyAIBrain.transform.position, _lastPosition) <= 0f)
                timeStuck += Time.deltaTime;
            
            _lastPosition = _enemyAIBrain.transform.position;
            _animator.SetFloat(Speed,_navMeshAgent.velocity.magnitude);
            
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.TurretTarget.position);
            _animator.SetTrigger(Run);
            _navMeshAgent.speed = 1.6f;
        }

        public void OnExit()
        {
        }
    }
}