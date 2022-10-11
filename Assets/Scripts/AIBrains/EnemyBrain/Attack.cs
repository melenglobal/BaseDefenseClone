using Abstract;
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
        private static readonly int Run = Animator.StringToHash("Run");

        private float _attackTimer = 1f;

        public Attack(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {

            _attackTimer -= Time.deltaTime;
            if (!(_attackTimer <= 0)) return;
            
            Debug.Log("ATTACK!");
            _animator.SetTrigger(Attack1);
            _attackTimer = 1f;

        }
        
        public void OnEnter()
        {   
            // _animator.SetTrigger(Attack1);
        }

        public void OnExit()
        {   
            _animator.SetTrigger(Run);
        }
    }
}