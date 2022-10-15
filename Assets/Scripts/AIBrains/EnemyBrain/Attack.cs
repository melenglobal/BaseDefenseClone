using Abstract;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Attack : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private static readonly int Attack1 = Animator.StringToHash("Attack");

        private const int _attackPower = 10;

        private float _attackTimer = 2f;

        public Attack(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {

            _attackTimer -= Time.deltaTime;
            if (!(_attackTimer <= 0)) return;
            
            CoreGameSignals.Instance.onTakeDamage?.Invoke(_attackPower);
            _animator.SetTrigger(Attack1);
            _attackTimer = 2f;

        }
        
        public void OnEnter()
        {
        }

        public void OnExit()
        {
            
        }
    }
}