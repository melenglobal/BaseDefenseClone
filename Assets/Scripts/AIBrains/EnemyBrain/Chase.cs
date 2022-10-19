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
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _run = Animator.StringToHash("Run");
        private const float _enemySpeed = 6.3f;
        private float _timer = 0.1f;

        public Chase(EnemyAIBrain enemyAIBrain,NavMeshAgent agent,Animator animator)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = agent;
            _animator = animator;
        }

        public void Tick()
        {

            _navMeshAgent.destination = _enemyAIBrain.CurrentTarget.position;
            _animator.SetFloat(_speed, _navMeshAgent.velocity.magnitude);
            _timer -= Time.deltaTime;
            if (!(_timer <= 0)) return;
            if (_enemyAIBrain.SoldierHealthController != null)
            {
                if (_enemyAIBrain.SoldierHealthController.IsDead)
                {
                    _enemyAIBrain.SetTarget(null);
                }
            }

            // if(_enemyAIBrain.PlayerPhysicsController)  // Player Is Dead scenario should be implemented.
            _timer = 0.1f;
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_enemyAIBrain.CurrentTarget.position);
            _animator.SetTrigger(_run);
            _navMeshAgent.speed = _enemySpeed;
        }
        public void OnExit()
        {
        }
    }
}