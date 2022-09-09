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
        public Death(NavMeshAgent agent,Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}