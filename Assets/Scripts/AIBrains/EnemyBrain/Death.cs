using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Death : IState
    {   
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        public int Health;
        public bool IsDead;
        private static readonly int Death1 = Animator.StringToHash("Death");

        public Death(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _animator.SetTrigger(Death1);
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}