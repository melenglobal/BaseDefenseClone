using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Attack : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private static readonly int Attack1 = Animator.StringToHash("Attack");

        public Attack(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
              
        }
        
        public void OnEnter()
        {
           _animator.SetTrigger(Attack1);
           
        }

        public void OnExit()
        {
      
        }
    }
}